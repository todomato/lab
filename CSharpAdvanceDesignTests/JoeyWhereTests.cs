using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using ExpectedObjects;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyWhereTests
    {
        [Test]
        public void find_products_that_price_between_200_and_500()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            //
            var actual = JoeyWhere(products);

            var expected = new List<Product>
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_products_that_price_between_200_and_500_and_cost_less_than_30()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = JoeyWhereCostLessThan30(products);

            var expected = new List<Product>
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private List<Product> JoeyWhereCostLessThan30(List<Product> products)
        {
            var result = new List<Product>();
            foreach (var product in products)
            {
                if (product.Price >= 200 && product.Price <= 500 && product.Cost < 30)
                {
                    result.Add(product);
                }
            }

            return result;
        }

        private List<Product> JoeyWhere(List<Product> products)
        {
            var result = new List<Product>();
            foreach (var product in products)
            {
                if (product.Price >= 200 && product.Price <= 500)
                {
                    result.Add(product);
                }
            }

            return result;
        }
    }
}