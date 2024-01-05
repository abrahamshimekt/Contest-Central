using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Application.Contracts.Identity;
using Application.Models.Identity;
using AutoMapper;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Domain;
using MediatR;
using Application.Models.Mail;
using Application.Contracts.Infrastructure.Mail;
using System.Web;
using Application.Contracts.Infrastructure.Photo;
using Microsoft.Extensions.Options;
using Application.Common.Responses;
using Application.Features.Authentication.Dto;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ContestCentralDbContext _context;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IEmailSender _emailSender;
        private readonly ServerSettings _serverSettings;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ServerSettings> serverSettings, IMapper mapper, IConfiguration configuration, ContestCentralDbContext context, IPhotoAccessor photoAccessor, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
            _photoAccessor = photoAccessor;
            _emailSender = emailSender;
            _serverSettings = serverSettings.Value;
        }



        // login and registration
        public async Task<CommandResponse<AuthResponse>> LoginAsync(AuthRequest model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(userExist, model.Password, false);

                if (result.Succeeded)
                {
                    var token = await GenerateToken(userExist);
                    var data = _mapper.Map<AuthResponse>(userExist);
                    data.Token = new JwtSecurityTokenHandler().WriteToken(token);

                    return CommandResponse<AuthResponse>.Success(data);

                }

            }

            return CommandResponse<AuthResponse>.Failure("Invalid Password or Email");
        }
        public async Task<CommandResponse<bool>> LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return CommandResponse<bool>.Success(true);
        }
        public async Task<CommandResponse<RegistrationResponse>> RegistrationAsync(RegistrationRequest model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {


                var userExist = await _userManager.FindByEmailAsync(model.Email);
                if (userExist == null)
                {
                    var user = _mapper.Map<ApplicationUser>(model);
                    if (model.ProfilePicture != null)
                    {
                        var photoUploadResult = await _photoAccessor.AddPhoto(model.ProfilePicture);
                        if (photoUploadResult == null)
                        {
                            return CommandResponse<RegistrationResponse>.Failure("Photo Upload Failed");

                        }

                        user.Photo = new Photo
                        {
                            Id = photoUploadResult.PublicId,
                            Url = photoUploadResult.Url,
                            UserId = user.Id
                        };
                    }
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Student");

                        var createdUser = await _userManager.FindByNameAsync(user.UserName);
                        var newUser = _mapper.Map<RegistrationResponse>(createdUser);
                        await transaction.CommitAsync();
                        var confirmResult = await sendConfirmEmailLink(createdUser.Email);
                        if (!confirmResult.IsSuccess)
                        {
                           return CommandResponse<RegistrationResponse>.Failure("Email not confirmed");
                        }
                        return CommandResponse<RegistrationResponse>.Success(newUser);
                    }
                    return CommandResponse<RegistrationResponse>.Failure("User Creation Failed");
                }
                return CommandResponse<RegistrationResponse>.Failure("User Already Exists");

            }
        }


        // send email confirmation

       
        public async Task<CommandResponse<string>> sendConfirmEmailLink(string Email)
        {

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return CommandResponse<string>.Failure("User doesn't exist");
            }


            if (user.EmailConfirmed)
            {
                return CommandResponse<string>.Failure("Email already confirmed");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string connectionLink = _serverSettings.BaseApiUrl + $"Account/Confirm/?email={HttpUtility.UrlEncode(Email)}&token={HttpUtility.UrlEncode(token)}";
            var message = new Email
            {
                To = Email,
                Subject = "Email Confirmation",
                Body = $"Email Confirmation link: {connectionLink}\n token: {token}"
            };

            var emailResult = await _emailSender.sendEmail(message);
            if (!emailResult.IsSuccess)
            {
                return CommandResponse<string>.Failure("Could not send Email!");
            }

            return CommandResponse<string>.Success("Email Sent");
        }


        public async Task<CommandResponse<string>> ForgotPassword(string Email)
        {
            var result = new CommandResponse<string>();
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return CommandResponse<string>.Failure("User doesn't exist");
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string resetLink = $"{_serverSettings.BaseApiUrl}/{token}";

            var message = new Email
            {
                To = Email,
                Subject = "Password Reset",
                Body = $"To reset your password, please click on this link {resetLink}"
            };

            var emailResult = await _emailSender.sendEmail(message);
            if (!emailResult.IsSuccess)
            {
                result.IsSuccess = false;
                result.Errors = emailResult.Errors;
                return result;
            }

            result.IsSuccess = true;
            result.Data = Email;
            return result;

        }

        public async Task<CommandResponse<string>> ResetPasswordAsync(string Email, string Token, string NewPassword)

        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return CommandResponse<string>.Failure("User doesn't exist");
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, Token, NewPassword);
            if (!resetPassResult.Succeeded)
            {
            
                return CommandResponse<string>.Failure("Password Reset Failed");
            }

            return CommandResponse<string>.Success("Password Reset Successful");
        }

         public async Task<CommandResponse<string>> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return CommandResponse<string>.Failure("User doesn't exist");
            }

            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
            {
                return CommandResponse<string>.Failure("Email Confirmation Failed");
            }

            return CommandResponse<string>.Success("Email Confirmed");

        }


        public async Task<CommandResponse<AuthResponse>> ChangeUserRoleAsync(string Email, string role)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return CommandResponse<AuthResponse>.Failure("User doesn't exist");
            }
            else
            {
                if (!new List<string> { "Administrator", "Student", "ContestCreator", "LeadHeadOfEduacation", "HeadOfEducation" }.Contains(role))
                {
                    return CommandResponse<AuthResponse>.Failure("Invalid Role");
                }


                var result = await _userManager.AddToRoleAsync(user, role);
                if (result.Succeeded)
                {
                    var data = _mapper.Map<AuthResponse>(user);
                    return CommandResponse<AuthResponse>.Success(data);
                }
                else
                {
                    return CommandResponse<AuthResponse>.Failure("User Role Update Failed");
                }
            }
        }
        // Generate token function

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
        }
            .Union(userClaims)
            .Union(roleClaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:Key")));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
               issuer: _configuration.GetValue<string>("JwtSettings:Issuer"),
               audience: _configuration.GetValue<string>("JwtSettings:Audience"),
               claims: claims,
               expires: DateTime.Now.AddMinutes(_configuration.GetValue<Int32>("JwtSettings:DurationInMinutes")),
               signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        // user functions
        public async Task<CommandResponse<AuthResponse>> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.FindFirstValue(ClaimTypes.Email));
            if (appUser == null)
            {
                return CommandResponse<AuthResponse>.Failure("User doesn't exist");
            }
            else
            {
                var token = await GenerateToken(appUser);
                var data = _mapper.Map<AuthResponse>(appUser);
                data.Token = new JwtSecurityTokenHandler().WriteToken(token);
                return CommandResponse<AuthResponse>.Success(data);
            }
        }

        public async Task<CommandResponse<UserDetailDto>> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return CommandResponse<UserDetailDto>.Failure("User doesn't exist");
            }
            else
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var data = _mapper.Map<UserDetailDto>(user);
                data.UserRole = userRoles;

                return CommandResponse<UserDetailDto>.Success(data);
            }
        }

        public async Task<CommandResponse<List<UserDto>>> GetAllUserAsync()
        {
            var users = _userManager.Users.ToList();
            if (users == null)
            {
                return CommandResponse<List<UserDto>>.Failure("No Users Found");
            }
            else
            {
                var data = _mapper.Map<List<UserDto>>(users);
                return CommandResponse<List<UserDto>>.Success(data);
            }



        }
        public async Task<CommandResponse<Unit>> DeleteUserAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return CommandResponse<Unit>.Failure("User doesn't exist");

            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return CommandResponse<Unit>.Success(Unit.Value);
                }
                else
                {
                    return CommandResponse<Unit>.Failure("User Deletion Failed");
                }
            }
        }




    }


}






