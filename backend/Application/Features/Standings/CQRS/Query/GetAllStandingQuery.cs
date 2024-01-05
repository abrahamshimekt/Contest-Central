using System;
using Application.Common.Query;
using Application.Common.Responses;
using Application.Features.Standings.Dtos;
using MediatR;

namespace Application.Features.Standings.CQRS.Query
{
    public class GetAllStandingQuery : PaginatedQuery,  IRequest<PaginatedResponse<StandingDto>>
    {
        
    }
}