using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using MediatR;

namespace Application.Features.Authentication.Commands
{
    public class LogOutCommand : IRequest<CommandResponse<bool>>
    {
        
    }
}