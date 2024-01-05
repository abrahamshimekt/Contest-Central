using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Models.Identity;
using MediatR;

namespace Application.Features.Authentication.Commands
{
    public class RegistrationCommand : IRequest<CommandResponse<RegistrationResponse>>
    {
        public RegistrationRequest RegistrationRequest { get; set; }
    }
}