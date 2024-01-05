using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Features.Contests.Dtos;
using MediatR;

namespace Application.Features.Contests.Commands
{
    public class CreateContestCommand : IRequest<CommandResponse<List<ProblemDto>>>
    {
        public CreateContestDto CreateContestDto {get;set;}
    }
}