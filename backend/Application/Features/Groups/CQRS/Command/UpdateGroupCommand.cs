using System;
using Application.Common.Responses;
using Application.Features.Groups.Dtos;
using MediatR;

namespace Application.Features.Groups.CQRS.Command
{
    public class UpdateGroupCommand : IRequest<CommandResponse<Unit>>
    {
        public required UpdateGroupDto UpdateGroupDto { get; set; }
    }
}