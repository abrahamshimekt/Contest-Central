using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
    new IdentityRole
    {
        Id = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
        Name = "Student",
        NormalizedName = "STUDENT"
    },
    new IdentityRole
    {
        Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
        Name = "Administrator",
        NormalizedName = "ADMINISTRATOR"
    },
    new IdentityRole
    {
        Id = "cb2f3a8e-f7bb-4445-baaf-1add431ffbbf",
        Name = "ContestCreator",
        NormalizedName = "CONTESTCREATOR"
    },
    new IdentityRole
    {
        Id = "cb4a3a8e-f7bb-4445-baaf-1add431ffbbf",
        Name = "LeadHeadOfEduacation",
        NormalizedName = "LEADHEADOFEDEUCATION"
    },
    new IdentityRole
    {
        Id = "cb5b3a8e-f7bb-4445-baaf-1add431ffbbf",
        Name = "HeadOfEducation",
        NormalizedName = "HEADOFEDEUCATION"
    }
);

        }
    }
}