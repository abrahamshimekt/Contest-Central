using System;
using Application.Common.Dtos;

namespace Application.Features.Standings.Dtos
{
    public class UpdateStandingDto : BaseDto
    {
        public string Id { get; set; }  
        public required Guid ContestId { get; set; }
        public required string UserHandle { get; set; }
        public int  SolvedProblems { get; set; }
        public int Rank {get; set;}

    }
}