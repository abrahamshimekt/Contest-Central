using System;
using Application.Common.Responses;
using Application.Features.Groups.Dtos;
using MediatR;

namespace Application.Features.Groups.CQRS.Query
{
    public class GetGroupQuery : IRequest<CommandResponse<GroupDto>>
    {
        public string Id { get; set; }
        
    }
}