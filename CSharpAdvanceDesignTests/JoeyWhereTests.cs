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
            var actual = JoeyWhere(products, product => product.Price >= 200 && product.Price <= 500);

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

            Func<Product, bool> predicate = product => product.Price >= 200 && product.Price <= 500 && product.Cost < 30;
            var actual = JoeyWhere(products, predicate);

            var expected = new List<Product>
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void Find_the_first_name_length_less_than_5()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Claire", LastName = "Chen"},
                new Employee {FirstName = "May", LastName = "Chen"},
            };

            Func<Employee, bool> predicate = e => e.FirstName.Length < 5;
            var actual = JoeyWhere<Employee>(employees, predicate);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "May", LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }


        private List<TSource> JoeyWhere<TSource>(List<TSource> sources, Func<TSource, bool> predicate)
        {
            var result = new List<TSource>();
            foreach (var item in sources)
            {
                // 不一樣的地方抽成參數, duplicate 壞味道 : 為了消除重複才用Func
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }


        //summary : 過濾商品不一樣,其他一樣,只有if條件不一樣,可以抽出參數
        //手動搬code 會有很多問題,用Inline method可以一次整理

        //使用 : extract prameter ctrl + r + p
        // inline method (variable field parameter class)

        // 鮮血測試來模擬 其他人如何使用api reshaprer 好處可以顯示

        // bad 方法太具體也不好 因為只支援那件事 部彈性

        // 3. 如果class不一樣product empliyee 第二格型別不一樣 其他一樣 使用泛型

        // 故意在<T> 改成product 讓IDE以為是T 再用rename
    }
}