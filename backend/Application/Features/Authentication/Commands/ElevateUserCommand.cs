using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Models.Identity;
using MediatR;

namespace Application.Features.Authentication.Commands
{
    public class ElevateUserCommand : IRequest<CommandResponse<AuthResponse>>
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}