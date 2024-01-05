 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Models.Identity;
using MediatR;

namespace Application.Features.Authentication.Commands
{
    public class sendConfirmEmailLinkCommand : IRequest<CommandResponse<string>>
    {
        
        public string Email { get; set; }   
        
    }
}