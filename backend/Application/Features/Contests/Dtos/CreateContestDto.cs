using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Features.Contests.Dtos
{
    public class CreateContestDto
    {
        public string ContestName {get;set;}
        public List<ProblemDto> ProblemDtos{get;set;}
        public List<string> Creators {get;set;}
        

    }
}