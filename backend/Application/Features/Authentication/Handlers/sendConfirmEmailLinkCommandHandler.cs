using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Identity;
using Application.Features.Authentication.Commands;
using Application.Models.Identity;
using MediatR;

namespace Application.Features.Users.Handlers
{
    public class sendConfirmEmailLinkCommandHandler : IRequestHandler<sendConfirmEmailLinkCommand, CommandResponse<string>>
    {
        private readonly IAuthService _authService;
        public sendConfirmEmailLinkCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<CommandResponse<string>> Handle(sendConfirmEmailLinkCommand request, CancellationToken cancellationToken)
        {
            var response =  await _authService.sendConfirmEmailLink(request.Email);
            return response;
        }
    }
}