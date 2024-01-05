using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain
{
    public class Photo : BaseDomainEntity
    {
        public string Id { get; set; }
        public string Url { get; set;}
        public string UserId { get; set;}
        
    }
}