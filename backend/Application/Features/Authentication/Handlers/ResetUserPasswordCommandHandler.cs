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
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, CommandResponse<string>>
    {
        private readonly IAuthService _authService;
        public ResetUserPasswordCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<CommandResponse<string>> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
           var response = await _authService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
           return response;
        }
    }
    
}