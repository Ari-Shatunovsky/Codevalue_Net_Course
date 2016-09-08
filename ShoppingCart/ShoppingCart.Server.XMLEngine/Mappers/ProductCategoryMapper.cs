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
    public class ProductCategoryMapper: EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMapper()
        {
            this.ToTable("ProductCategories");

            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Name).IsRequired();
            this.Property(c => c.Name).HasMaxLength(255);

            //this.HasOptional(e.)
        }
    }
}
