using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine
{
    public class SimilarProductSearchEngine
    {
        private readonly Regex _regexMl;
        private readonly Regex _regexL;
        private readonly Regex _regexU;
        private readonly Regex _regexG;
        private readonly Regex _regexKg;

        public SimilarProductSearchEngine() 
        {
             _regexMl = new Regex(@"(\d+) ?(מ)");
             _regexL = new Regex(@"((?:[1-9]\d*|0)?(?:\.\d+)) ?(ל)");
            _regexU = new Regex(@"(\d+)");
            _regexG = new Regex(@"(\d+) ?(ג`?ר?)");
            _regexKg = new Regex(@"((?:[1-9]\d*|0)?(?:\.\d+)) ?(ק)");
        }


        public Product SearchById(ICollection<Product> products, Product product)
        {
            return products.FirstOrDefault(p => p.ProductId == product.ProductId);
        }

        public Product Search(ICollection<Product> products, Product product) =>SearchById(products, product) ?? SearchByName(products, product).FirstOrDefault();

        public ICollection<Product> SearchByName(ICollection<Product> products, Product product)
        {
//            var result = new List<RangedSearch>();
//            result = products.Where(p => GetNamesSimilarity(p.Name, product.Name) > 30).
//                Select(p => new RangedSearch() {Product = product, Similarity = GetNamesSimilarity(p.Name, product.Name) }).ToList();
            var similaProducts = products.Where(p => GetNamesSimilarity(p.Name, product.Name) > 40).OrderBy(p => -GetNamesSimilarity(p.Name, product.Name)).ToList();
            return similaProducts;
        }

        private int GetNamesSimilarity(string name1, string name2)
        {

            name1 = _regexG.Replace(name1, "");
            name1 = _regexMl.Replace(name1, "");
            name1 = _regexL.Replace(name1, "");
            name1 = _regexU.Replace(name1, "");
            name1 = _regexKg.Replace(name1, "");

            name2 = _regexG.Replace(name2, "");
            name2 = _regexMl.Replace(name2, "");
            name2 = _regexL.Replace(name2, "");
            name2 = _regexU.Replace(name2, "");
            name2 = _regexKg.Replace(name2, "");

            char[] delimiters = new[] {',', ';', ' ', '-'};
            var words1 = name1.Split(delimiters);
            var words2 = name2.Split(delimiters);
            var matchCount = words2.Sum(word => words1.Contains(word) ? word.Length : 0);
            return matchCount * 200 / (name1.Length + name2.Length) ;
        }
    }
}
