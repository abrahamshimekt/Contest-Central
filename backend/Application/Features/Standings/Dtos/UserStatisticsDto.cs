using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Features.Standings.Dtos
{
    public class UserStatisticsDto
    {
        public int NumberOfProblemsSolved {get;set;}
        public int TotalContestProblems {get;set;}
        public int NumberOfContestsParticipated {get;set;}
    }
}