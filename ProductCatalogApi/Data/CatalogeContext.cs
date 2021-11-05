using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Domain;

namespace ProductCatalogApi.Data
{
    public class CatalogeContext :DbContext  
    {
        public DbSet<CatalogType>  CatalogeTypes { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogItem> Catalog { get; set; }

        public CatalogeContext(DbContextOptions options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<CatalogType>(e => {
                    e.Property(t => t.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                    e.Property(t => t.Type)
                    .IsRequired()
                    .HasMaxLength(100);

                });



                modelBuilder.Entity<CatalogBrand>(e =>
               {
                   e.Property(b => b.Brand)
                     .IsRequired()
                     .HasMaxLength(100);
                   e.Property(b => b.Id)
                     .IsRequired()
                     .ValueGeneratedOnAdd();

               });


                modelBuilder.Entity<CatalogItem>(e =>
                {
                    e.Property(c => c.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd();

                    e.Property(c => c.Name)
                        .IsRequired()
                        .HasMaxLength(100);

                    e.Property(c => c.Price)
                        .IsRequired();

                    e.HasOne(C => C.CatalogType)
                        .WithMany()
                        .HasForeignKey(C => C.CatalogTypeId);
                    e.HasOne(c => c.CatalogBrand)
                        .WithMany()
                        .HasForeignKey(c => c.CatalogBrandId);
                });
            
            
            
        
                
                
                
             }
    }
}
