using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseDomainEntity
    {
        public Guid Id { get; set;}
        public DateTime LastModifiedDate { get; set;}
        public DateTime DateCreated { get; set;}

    }
}