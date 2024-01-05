using System;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using Domain.Common;

namespace Domain
{
    public class Group : BaseDomainEntity
    {
         public required string Id {get;set;}
        public string Name { get; set; }
        public List<ApplicationUser> Students {get;set;}

        
    }
}