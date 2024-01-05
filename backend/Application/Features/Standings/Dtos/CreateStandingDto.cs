using System;

namespace Application.Features.Standings.Dtos
{
    public class CreateStandingDto 
    {
       public required Guid ContestId { get; set; }
        public required string UserHandle { get; set; }
        public int  SolvedProblems { get; set; }
        public int Rank {get; set;}
    }
}