using System;
using Application.Common.Responses;
using MediatR;

namespace Application.Features.Groups.CQRS.Command
{
    public class DeleteGroupCommand : IRequest<CommandResponse<Unit>>
    {
        public string Id { get; set; }
        
    }
}