using System;
using Application.Common.Responses;
using Application.Features.Groups.Dtos;
using MediatR;

namespace Application.Features.Groups.CQRS.Command
{
    public class CreateGroupCommand : IRequest<CommandResponse<string>>
    {
        public required CreateGroupDto CreateGroupDto { get; set; }

    }
}