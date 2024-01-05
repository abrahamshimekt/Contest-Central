using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain
{
    public class Standing : BaseDomainEntity
    {
        public required Guid ContestId { get; set; }
        public Contest Contest { get; set; }
        public required string UserHandle { get; set; }
        public int  SolvedProblems { get; set; }
        public int Rank {get; set;}
    }
}




