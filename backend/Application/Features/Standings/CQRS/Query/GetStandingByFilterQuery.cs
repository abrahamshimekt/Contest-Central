using System;
using Application.Common.Responses;
using Application.Features.Standings.Dtos;
using MediatR;

namespace Application.Features.Standings.CQRS.Query
{
    public class GetStandingByFilterQuery : IRequest<CommandResponse<IReadOnlyList<StandingDto>>>
    {
        public Guid ContestId { get; set; }
        public string? University { get; set; } 
        public string? GroupId { get; set; }


    }
}