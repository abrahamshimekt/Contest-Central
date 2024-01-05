using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Identity;
using Application.Features.Authentication.Commands;
using Application.Models.Identity;
using MediatR;

namespace Application.Features.Authentication.Handlers
{
    public class ElevateUserCommandHandler : IRequestHandler<ElevateUserCommand, CommandResponse<AuthResponse>>
    {
        private readonly IAuthService _authService;
        public ElevateUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<CommandResponse<AuthResponse>> Handle(ElevateUserCommand request, CancellationToken cancellationToken)
        {

            var response = await _authService.ChangeUserRoleAsync(request.Email, request.Role);
            return response;
        }
    }
}