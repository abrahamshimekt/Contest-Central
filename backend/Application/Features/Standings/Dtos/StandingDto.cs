using System;
using Application.Common.Dtos;

namespace Application.Features.Standings.Dtos
{
    public class StandingDto 
    {
        public Guid Id { get; set; }
        public  Guid ContestId { get; set; }
        public  string UserHandle { get; set; }
        public int  SolvedProblems { get; set; }
        public int Rank {get; set;}

        
    }
}