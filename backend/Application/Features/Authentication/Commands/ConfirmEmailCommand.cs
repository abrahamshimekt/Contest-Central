using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using MediatR;

namespace Application.Features.Authentication.Commands
{
    public class ConfirmEmailCommand : IRequest<CommandResponse<string>>
    {
        public string Email {get;set;}
        public string Token {get;set;}
    }
}