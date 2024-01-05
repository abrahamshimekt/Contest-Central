using Application.Common.Responses;
using Application.Contracts.Identity;
using Application.Features.Authentication.Commands;
using MediatR;

namespace Application.Features.Authentication.Handlers
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, CommandResponse<string>>
    {
        private readonly IAuthService _authService;
        public ConfirmEmailCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<CommandResponse<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.ConfirmEmail(request.Email, request.Token);
            return response;
        }
    }
}