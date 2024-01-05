using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Identity;
using Application.Features.Authentication.Commands;
using MediatR;

namespace Application.Features.Authentication.Handlers
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, CommandResponse<string>>
    {
        private readonly IAuthService _authService;
        public ForgetPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }   
        public async Task<CommandResponse<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.ForgotPassword(request.Email);
            return response;
            
        }
    }
   
}