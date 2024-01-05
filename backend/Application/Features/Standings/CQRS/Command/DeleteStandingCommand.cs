using System;
using MediatR;

namespace Application.Features.Standings.CQRS.Command
{
    public class DeleteStandingCommand : IRequest<Unit>
    {
        public int Id {get; set;}
    }
}