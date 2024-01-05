using System.Net;
using Application.Common.Responses;
using Application.Features.Authentication.Commands;
using Application.Features.Authentication.Dto;
using Application.Features.Authentication.Queries;

using Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator) : base(mediator){
            _mediator = mediator;
        }
    
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<CommandResponse<RegistrationResponse>>> Register([FromForm] RegistrationRequest registrationRequest)
        {
            var command = new RegistrationCommand { RegistrationRequest = registrationRequest };
            var result = await _mediator.Send(command);
            var status = result.IsSuccess? HttpStatusCode.Created: HttpStatusCode.BadRequest;
            return HandleCommand(status,result);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<CommandResponse<AuthResponse>>> Login([FromForm] AuthRequest authRequest)
        {
            var command = new AuthQuery { AuthRequest = authRequest };
            var result = await _mediator.Send(command);
            if (result.IsSuccess){
                HttpContext.Session.SetString("user id", result.Data!.Id); // Example: Storing user ID in session
               

            }
            var status = result.IsSuccess? HttpStatusCode.OK: HttpStatusCode.BadRequest;
            return HandleCommand(status,result);
        }


        [HttpPost]
        [Route("Logout")]
        [Authorize]
        public async Task<ActionResult<CommandResponse<bool>>> Logout()
        {
            var command = new LogOutCommand();
            var result = await _mediator.Send(command);
            var status = result.IsSuccess? HttpStatusCode.OK:HttpStatusCode.BadRequest;
            return HandleCommand(status,result);
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        [Authorize(Roles = "Administrator")] 
        public async Task<ActionResult<CommandResponse<Unit>>> DeleteUser(string id)
        {
            var command = new DeleteUserCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeRole")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CommandResponse<AuthResponse>>> ElevateUser(string Email, string role)
        {
            var command = new ElevateUserCommand { Email = Email, Role = role };
            var result = await _mediator.Send(command);
            var status = result.IsSuccess? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            return HandleCommand(status,result);
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        [Authorize]
        public async Task<ActionResult<CommandResponse<UserDetailDto>>> GetUserById(string id){
            var command = new GetUserDetailQuery{UserId=id};
            var result = await _mediator.Send(command);
            var status = result.IsSuccess? HttpStatusCode.OK: HttpStatusCode.NotFound;
            return HandleCommand(status,result);

        }


        [HttpPost]
        [Authorize]
        [Route("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            var command = new ResetUserPasswordCommand { Email = resetPasswordRequest.Email,Token=resetPasswordRequest.Token,NewPassword=resetPasswordRequest.NewPassword };
            var result = await _mediator.Send(command);
            var status = result.IsSuccess? HttpStatusCode.OK: HttpStatusCode.BadRequest;
            return HandleCommand(status,result);
        }
       

        [HttpGet]
        [Route("GetUsers")]
        [Authorize]
        public async Task<ActionResult<CommandResponse<List<UserDto>>>> GetUsers()
        {
            var command = new GetUsersListQuery();
            var result = await _mediator.Send(command);
            var status = result.IsSuccess? HttpStatusCode.OK: HttpStatusCode.NotFound;
            return HandleCommand(status,result);
        }

       [HttpPost]
       [Route("ForgotPassword")]
       [AllowAnonymous]
       public async Task<ActionResult<CommandResponse<string>>> ForgetPassword(string Email){
        var command = new ForgetPasswordCommand{Email=Email};
        var result = await _mediator.Send(command);
        var status = result.IsSuccess? HttpStatusCode.OK: HttpStatusCode.BadRequest;
        return HandleCommand(status,result);
       }

       [HttpGet]
       [Route("ResendConfirmLink")]
       [AllowAnonymous]
       public async Task<ActionResult<CommandResponse<string>>> ResendConfirmEmail(string Email){
        var command = new sendConfirmEmailLinkCommand{Email=Email};
        var result = await _mediator.Send(command);
        var status = result.IsSuccess? HttpStatusCode.OK: HttpStatusCode.BadRequest;
        return HandleCommand(status,result);
       }

      [HttpGet]
      [Route("Confirm")]
      [AllowAnonymous]
      public async Task<ActionResult<CommandResponse<string>>> ConfirmEmail(string Email,string Token){
        var command = new ConfirmEmailCommand{Email=Email,Token=Token};
        var result = await _mediator.Send(command);
        var status = result.IsSuccess? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        return HandleCommand(status,result);
      }

    }
}






