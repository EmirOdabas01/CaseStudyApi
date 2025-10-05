using CaseStudyApi.Domain.Entities;
using CaseStudyApi.Domain.Entities.Common;
using CaseStudyApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public DbSet<ProductColor> ProductColors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImageFile>()
                .HasOne(p => p.ProductColor)
                .WithMany(pc => pc.ProductImageFiles)
                .HasForeignKey(p => p.ProductColorId);
               

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(pı => pı.Product);

            modelBuilder.Entity<ProductColor>()
                .HasData(
                    new { Id = 1, Color = "Yellow"},
                    new { Id = 2, Color = "Yellow Orange"},
                    new { Id = 3, Color = "Orange" },
                    new { Id = 4, Color = "Red Orange" },
                    new { Id = 5, Color = "Red" },
                    new { Id = 6, Color = "Red Violet" },
                    new { Id = 7, Color = "Violet" },
                    new { Id = 8, Color = "Blue Violet" },
                    new { Id = 9, Color = "Blue" },
                    new { Id = 10, Color = "Blue Green" },
                    new { Id = 11, Color = "Green" },
                    new { Id = 12, Color = "Yellow Green" },
                    new { Id = 13, Color = "Black" },
                    new { Id = 14, Color = "White" }
                );

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
