using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.Identity;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                //ContestCreator
                 new ApplicationUser
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     GroupId ="43",
                     Email = "biruk@a2sv.org",
                     NormalizedEmail = "BIRUK@A2SV.ORG",
                     FirstName = "Biruk",
                     LastName = "Tedla",
                     UserName = "metalic",
                     UniversityName = "AAiT",
                     NormalizedUserName = "METALIC",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true
                 },
                 //Student
                 new ApplicationUser
                 {
                     Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                    GroupId = "44",
                     Email = "abrham@a2sv.org",
                     NormalizedEmail = "ABRHAM@A2SV.ORG",
                     FirstName = "Abraham",
                     LastName = "Shimekt",
                     UniversityName = "AAstu",
                     UserName = "beasty",
                     NormalizedUserName = "BEASTY",
                     PasswordHash = hasher.HashPassword(null, "P@ssword2"),
                     EmailConfirmed = true
                },
                    //HeadOfEducation
                   new ApplicationUser
                  {
                      Id = "cb5b3a8e-f7bb-4445-baaf-1add431ffbbf",
                      GroupId = "43",
                      Email = "segni.desalegn@a2sv.org",
                      NormalizedEmail = "SEGNI.DESALEGN@A2SV.ORG",
                      FirstName = "Segni",
                      LastName = "Desalegn",
                      UniversityName = "AAIT",
                      UserName = "segnigeek",
                      NormalizedUserName = "SEGNIGEEK",
                      PasswordHash = hasher.HashPassword(null,"P@ssword2"),
                      EmailConfirmed = true
                  },
                     //LeadHeadOfEducation
                  new ApplicationUser
                  {
                      Id = "cb4a3a8e-f7bb-4445-baaf-1add431ffbbf",
                        GroupId ="42",
                      Email = "biruk.zewdu@a2sv.org",
                      NormalizedEmail = "BIRUK.ZEWDU@A2SV.ORG",
                      FirstName = "Biruk",
                      LastName = "Zewdu",
                      UniversityName = "AAIT",
                      UserName = "birukzedu",
                      NormalizedUserName = "BIRUKZEWDU",
                      PasswordHash = hasher.HashPassword(null, "P@ssword2"),
                      EmailConfirmed = true
                  },
                    //Administrator
                  new ApplicationUser
                  {
                      Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                      GroupId = "41",
                      Email = "emre@a2sv.org",
                      NormalizedEmail = "EMRE@A2SV.ORG",
                      FirstName = "Biruk",
                      LastName = "Zewdu",
                      UniversityName = "AAIT",
                      UserName = "zhanzhad",
                      NormalizedUserName = "ZHANZHAD",
                      PasswordHash = hasher.HashPassword(null, "P@ssword2"),
                      EmailConfirmed = true
                  }
            );
        }
    }
}



