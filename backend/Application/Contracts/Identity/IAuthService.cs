using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Features.Authentication.Dto;
using Application.Models.Identity;
using MediatR;

namespace Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<CommandResponse<RegistrationResponse>> RegistrationAsync(RegistrationRequest model);
        Task<CommandResponse<AuthResponse>> LoginAsync(AuthRequest model);
        Task<CommandResponse<Unit>> DeleteUserAsync(string id);
        Task<CommandResponse<AuthResponse>> ChangeUserRoleAsync(string userId, string role);
        Task<CommandResponse<UserDetailDto>> GetUserByIdAsync(string userId);
        Task<CommandResponse<string>> ResetPasswordAsync(string Email, string Token, string  NewPassword);
        Task<CommandResponse<List<UserDto>>> GetAllUserAsync();
        Task<CommandResponse<bool>> LogOutAsync();
        Task<CommandResponse<string>> ForgotPassword(string Email);
        Task<CommandResponse<string>> sendConfirmEmailLink(string Email);
        Task<CommandResponse<string>> ConfirmEmail(string token, string email);


    }
}