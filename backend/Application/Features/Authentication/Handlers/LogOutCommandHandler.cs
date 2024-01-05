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
    public class LogOutCommandHandler : IRequestHandler<LogOutCommand, CommandResponse<bool>>
    {
        private readonly IAuthService _authService;
        public LogOutCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public Task<CommandResponse<bool>> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            var response = _authService.LogOutAsync();
            return response;
        }
    }
}