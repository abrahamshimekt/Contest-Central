using System;
using Application.Common.Responses;
using Application.Features.Standings.Dtos;
using MediatR;

namespace Application.Features.Standings.CQRS.Command
{
    public class UpdateStandingCommand : IRequest<CommandResponse<Unit>>
    {
        public UpdateStandingDto UpdateStandingDto { get; set; }
    }
}