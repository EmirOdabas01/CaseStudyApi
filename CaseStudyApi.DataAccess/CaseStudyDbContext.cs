using CaseStudyApi.Domain.Entities;
using CaseStudyApi.Domain.Entities.Common;
using CaseStudyApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.DataAccess
{
    public class CaseStudyDbContext : IdentityDbContext<AppUser, AppRole,int>
    {
        public CaseStudyDbContext(DbContextOptions<CaseStudyDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(pı => pı.Product);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach(var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };

            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
