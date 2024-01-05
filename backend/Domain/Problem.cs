using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain
{
    public class Problem :BaseDomainEntity
    {
        public string Id{get;set;}
        public string Url {get;set;}
        public Guid ContestId{get;set;}
        public Contest Contest {get;set;}
    
    }
}