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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandResponse<Unit>>
    {

        private readonly IAuthService _authService;
        public DeleteUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<CommandResponse<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.DeleteUserAsync(request.Id);
           return response;
        }
    }
}