using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Seed
{
    public class SeedData:IEntityTypeConfiguration<Group>
    {

        
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasData(
                new Group { Id = "41", Name = "group41" },
                new Group { Id = "42", Name = "group42" },
                new Group { Id = "43", Name = "group43" },
                new Group { Id = "44", Name = "group44" }
            );
        }

       
    }
}


