using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain
{
    public class Contest : BaseDomainEntity
    {
        public string ContestName {get;set;}
        public List<Problem> Problems {get;set;}
        public List<Standing> Standings {get;set;}
        public List<string> Creators {get;set;} = new List<string>();
        
    }
}