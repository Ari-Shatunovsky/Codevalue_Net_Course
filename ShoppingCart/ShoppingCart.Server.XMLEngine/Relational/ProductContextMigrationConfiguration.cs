using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Server.XMLEngine.Relational
{
    public class ProductContextMigrationConfiguration : DbMigrationsConfiguration<ProductContext>
    {
        public ProductContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
        
        protected override void Seed(ProductContext context)
        {
            new ProductDataSeeder(context).Seed();
        }

        //#if DEBUG
        //        protected override void Seed(ProductContext context)
        //        {
        //            new ProductDataSeeder(context).Seed();
        //        }
        //#endif
    }

}
