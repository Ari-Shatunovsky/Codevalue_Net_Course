using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine.Mappers
{
    class ProductMapper : EntityTypeConfiguration<Product>
    {
        public ProductMapper()
        {
            this.ToTable("Products");

            //this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Id).IsRequired();

            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Name).HasMaxLength(255);

            this.Property(p => p.Price).IsRequired();
            this.Property(p => p.Quantity).IsRequired();
            this.Property(p => p.ProductId).IsRequired();

            this.Property(p => p.ManufactureName).IsOptional();
            this.Property(p => p.ManufactureName).HasMaxLength(255);

            this.Property(p => p.ManufactureCountry).IsOptional();
            this.Property(p => p.ManufactureCountry).HasMaxLength(255);

            this.HasOptional(p => p.Category).WithMany().Map(c => c.MapKey("CategoryId"));
            this.HasOptional(p => p.Shop).WithMany().Map(c => c.MapKey("ShopId"));
            this.HasMany(p => p.SimilarProducts).WithMany(p => p.OtherSimilarProducts).Map(c =>
            {
                c.MapLeftKey("SimilarProductId");
                c.MapRightKey("OtherSimilarProductId");
                c.ToTable("SimilarProducts");
            });
//            this.HasRequired(p => p.Shop).WithRequiredDependent();
        }
    }
}
