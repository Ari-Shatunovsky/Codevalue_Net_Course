using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Server.XMLEngine.Entities;
using ShoppingCart.Server.XMLEngine.Relational;

namespace ShoppingCart.Server.XMLEngine
{

    public class Categorizer
    {
        private ProductContext _ctx = new ProductContext();
        private ICollection<ProductCategory> _categories;
        public static ProductCategory Meat { get; set; } = new ProductCategory() { Id = 1, Name = "Meat" };
        public static ProductCategory Fruits { get; set; } = new ProductCategory() { Id = 2, Name = "Fruits" };
        public static ProductCategory Vegetables { get; set; } = new ProductCategory() { Id = 3, Name = "Vegetables" };
        public static ProductCategory Diary { get; set; } = new ProductCategory() { Id = 4, Name = "Diary" };
        public static ProductCategory Bread { get; set; } = new ProductCategory() { Id = 5, Name = "Bread" };
        public static ProductCategory Alcohol { get; set; } = new ProductCategory() { Id = 6, Name = "Alcohol" };
        public static ProductCategory Toiletries { get; set; } = new ProductCategory() { Id = 7, Name = "Toiletries" };
        private static readonly IDictionary<ProductCategory, ContainsCategory> CategoryFilters
    = new Dictionary<ProductCategory, ContainsCategory>()
    {
                {Meat, (p) => p.Name.Contains("בשר")
                                   || p.Name.Contains("בקר") || p.Name.Contains("עוף")
                                   || p.Name.Contains("הודו") || p.Name.Contains("כבש")
                },
                {Fruits, (p) => (p.Name.Contains("אפרסק")
                                   || p.Name.Contains("אגס") || p.Name.Contains("אבטיח")
                                   || p.Name.Contains("אננס") || (p.Name.Contains("תפוח") && !p.Name.Contains("אדמה"))
                                   || p.Name.Contains("לימון") || p.Name.Contains("שזיף")
                                   || p.Name.Contains("מלון") || p.Name.Contains("תפוז")
                                   || p.Name.Contains("מנגו")) && p.Units == Units.Kilogramm
                },
                {Vegetables, (p) => (p.Name.Contains("עגבנ") || p.Name.Contains("בצל")
                                   || p.Name.Contains("כרוב") || p.Name.Contains("חציל")
                                   || p.Name.Contains("תפוח אדמה") || p.Name.Contains("פלפל")
                                   || p.Name.Contains("סלק") || p.Name.Contains("מלפפון")
                                   || p.Name.Contains("ארטישוק") || p.Name.Contains("גזר")) && p.Units == Units.Kilogramm
                },
                {Diary, (p) => p.Name.Contains("חלב") || p.Name.Contains("גבינ")
                                   || p.Name.Contains("יוגו") || p.Name.Contains("לבן")
                                   || p.Name.Contains("חמאה") || p.Name.Contains("קוטג")
                },
                {Bread, (p) => p.Name.Contains("לחם") || p.Name.Contains("פיתה")
                                   || p.Name.Contains("לחמ") || p.Name.Contains("בגט")
                                   || p.Name.Contains("בצק") || p.Name.Contains("עוגת")
                                   || p.Name.Contains("עוגה")
                },
                {Alcohol, (p) => (p.Name.Contains("בירה") || p.Name.Contains(" יין") || p.Name.StartsWith("יין")
                                   || p.Name.Contains("וודקה") || p.Name.Contains("ליקר")) && p.Units == Units.Liter
                },
                {Toiletries, (p) => p.Name.Contains("סבון") || p.Name.Contains("שמפו")
                                   || p.Name.Contains("מגבו") || p.Name.Contains("כביס")
                                   || p.Name.Contains("שניים") || p.Name.Contains("קרם")
                                   || p.Name.Contains("טישו")
                },
    };

        private delegate bool ContainsCategory(Product product);

        public Categorizer()
        {
            _categories = _ctx.ProductCategories.ToList();
        }

        public ProductCategory GetCategoryForProduct(Product product)
        {
            return (CategoryFilters.Where(c => c.Value(product)).Select(c => c.Key).FirstOrDefault());
        }

        public static ICollection<ProductCategory> Categories => CategoryFilters.Keys;

    }
}
