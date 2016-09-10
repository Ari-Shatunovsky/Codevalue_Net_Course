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
    class CartMapper : EntityTypeConfiguration<Cart>
    {
        public CartMapper()
        {
            this.ToTable("Carts");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Name).IsRequired();
            this.Property(c => c.Name).HasMaxLength(255);

            this.HasOptional(p => p.Shop).WithMany().Map(c => c.MapKey("ShopId"));

            this.HasMany(p => p.Products).WithMany().Map(c =>
            {
                c.MapLeftKey("ProductId");
                c.MapRightKey("CartId");
                c.ToTable("CartsProducts");
            });
        }
    }
}
