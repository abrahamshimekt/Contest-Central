using System;
using Application.Common.Responses;
using Application.Features.Standings.Dtos;
using MediatR;

namespace Application.Features.Standings.CQRS.Command
{
    public class CreateStandingCommand : IRequest<CommandResponse<Guid>>
    {
        public CreateStandingDto CreateStandingDto { get; set; }
        
        
    }
}