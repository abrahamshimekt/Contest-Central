using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Identity;
using Application.Features.Authentication.Queries;
using Application.Models.Identity;
using MediatR;

namespace Application.Features.Authentication.Handlers
{
    public class AuthQueryHandler : IRequestHandler<AuthQuery, CommandResponse<AuthResponse>>
    {

        private readonly IAuthService _authService;
        public AuthQueryHandler(IAuthService authService){
            _authService = authService;
        }

            public async Task<CommandResponse<AuthResponse>> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            var response = await _authService.LoginAsync(request.AuthRequest);
            return response;
        }
    }
}