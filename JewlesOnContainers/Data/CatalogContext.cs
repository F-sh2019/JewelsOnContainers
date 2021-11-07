using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogApi.Data
{
    public class CatalogContext:DbContext
    {

        public CatalogContext(DbContextOptions options) : base(options)
        { }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogType>(e =>
                {
                    e.Property(t => t.Id).IsRequired()
                                    .ValueGeneratedOnAdd();

                    e.Property(t => t.Type).HasMaxLength(100);
                });

            modelBuilder.Entity<CatalogBrand>(e =>
            {
                e.Property(t => t.Id).IsRequired()
                                     .ValueGeneratedOnAdd();

                e.Property(t => t.Brand).HasMaxLength(100);
            });

            modelBuilder.Entity<CatalogItem>(e =>
            {

                e.Property(c => c.Id).IsRequired()
                                    .ValueGeneratedOnAdd();

                e.Property(c => c.Name).IsRequired()
                                    .HasMaxLength(100);


                e.Property(c => c.Price).IsRequired();


                e.HasOne(c => c.CatalogType).WithMany().HasForeignKey(c => c.CatalogTypeId);

                e.HasOne(C => C.CatalogBrand).WithMany().HasForeignKey(c => c.CatalogBrandId);
            });


           
        }
    }
}
