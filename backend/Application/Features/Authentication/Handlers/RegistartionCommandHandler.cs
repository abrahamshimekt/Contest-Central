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
     
    public class RegistartionCommandHandler : IRequestHandler<RegistrationCommand, CommandResponse<RegistrationResponse>>
    {
         private readonly IAuthService _authService;
            public RegistartionCommandHandler(IAuthService authService){
        _authService = authService;
    } 
        public async Task<CommandResponse<RegistrationResponse>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var validator = new ValidateRegistrationRequest();
            var validationResult = await validator.ValidateAsync(request.RegistrationRequest);
            if (!validationResult.IsValid)
            {
                return new CommandResponse<RegistrationResponse>
                {
                    Message = "Registration failed",
                    IsSuccess = false,
                };
            }
            var response = await _authService.RegistrationAsync(request.RegistrationRequest);
            return response;
        }

    }
}