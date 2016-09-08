using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine.Mappers
{
    public class ShopInfoMapper: EntityTypeConfiguration<ShopInfo>
    {
        public ShopInfoMapper()
        {
            this.ToTable("Shops");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.Name).IsRequired();
            this.Property(s => s.Name).HasMaxLength(255);

            this.Property(s => s.BranchId).IsRequired();
            this.Property(s => s.Brand).IsRequired();
        }
    }
}