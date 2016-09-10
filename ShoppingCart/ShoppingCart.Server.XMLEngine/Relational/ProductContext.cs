using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Server.XMLEngine.Entities;
using ShoppingCart.Server.XMLEngine.Mappers;

namespace ShoppingCart.Server.XMLEngine.Relational
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("ShoppingCartDbConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductContext>());
        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<ShopInfo> Shops { get; set; } 
        public DbSet<ProductCategory> ProductCategories{ get; set; }
        public DbSet<Cart> Carts{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMapper());
            modelBuilder.Configurations.Add(new ShopInfoMapper());
            modelBuilder.Configurations.Add(new ProductCategoryMapper());
            modelBuilder.Configurations.Add(new CartMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}
