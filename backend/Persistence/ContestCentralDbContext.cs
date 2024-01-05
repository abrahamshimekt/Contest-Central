using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Seed;
using Domain;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ContestCentralDbContext : DbContext
    {

        public ContestCentralDbContext(DbContextOptions<ContestCentralDbContext> options) : base(options)
        {

        }

        //dbsets

        public DbSet<Standing> Standings { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<ApplicationUser> Users{get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContestCentralDbContext).Assembly);

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
       {
           entity.HasKey(ur => new { ur.UserId, ur.RoleId });


       });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
             {
                 entity.ToTable("UserClaims"); // Define the table name if needed
                 entity.HasKey(uc => uc.Id); // Set the primary key

             });
            // modelBuilder.ApplyConfiguration(new SeedData());

            modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Photo)
            .WithMany()
            .HasForeignKey(u => u.PhotoId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Contest>()
            .HasMany(c => c.Problems)
            .WithOne(p => p.Contest)
            .HasForeignKey(p => p.ContestId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contest>()
               .HasMany(s => s.Standings) 
               .WithOne(c => c.Contest)
               .HasForeignKey(s => s.ContestId)  
               .OnDelete(DeleteBehavior.Cascade); 


            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Group) 
                .WithMany(g => g.Students) 
                .HasForeignKey(u => u.GroupId) 
                .OnDelete(DeleteBehavior.SetNull); 


        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentTimeUtc = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = currentTimeUtc;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = currentTimeUtc;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}




