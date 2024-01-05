using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Models.Identity;
using MediatR;

namespace Application.Features.Authentication.Queries
{
    public class AuthQuery : IRequest<CommandResponse<AuthResponse>>
    {
        public AuthRequest AuthRequest { get; set; }
        
    }
}