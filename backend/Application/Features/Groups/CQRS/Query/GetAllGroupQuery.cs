using System;
using Application.Common.Query;
using Application.Common.Responses;
using Application.Features.Groups.Dtos;
using MediatR;

namespace Application.Features.Groups.CQRS.Query
{
    public class GetAllGroupQuery : PaginatedQuery, IRequest<PaginatedResponse<GroupDto>>
    {
        
    }
}