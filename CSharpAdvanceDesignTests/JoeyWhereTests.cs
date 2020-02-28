using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab;

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
            var actual = products.JoeyWhere(product => product.Price >= 200 && product.Price <= 500);

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
            var actual = products.JoeyWhere(predicate);

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
            var actual = employees.JoeyWhere(predicate);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "May", LastName = "Chen"},
            };

            // match (不管type 只比裡面內容 ex. list 比 array) vs equal (型別必須一樣)
            expected.ToExpectedObject().ShouldMatch(actual);
        }


        //summary :
        //過濾商品不一樣,其他一樣,只有if條件不一樣,可以抽出參數
        //手動搬code 會有很多問題,用Inline method可以一次整理

        // Day1使用 :
        // extract prameter ctrl + r + p
        // inline method (variable field parameter class)
        // extract class
        // move folder
        // convert static method to extendtion method 

        // *寫測試來模擬 其他人如何使用api reshaprer 好處可以顯示
        // bad: 方法太具體也不好 因為只支援那件事 部彈性
        // 3. 如果class不一樣product employee 第二格型別不一樣 其他一樣 使用泛型

        // 4.rename : 故意在<T> 改成product 讓IDE以為是T 再用rename

        // 5. LinqExtensions.JoeyWhere 1.將JoeyWhere抽成方法 2.抽到class 先+static 然後 快捷鍵 extact class
        // class 本身沒有domain意義, 又太長太蠢
        // 如果其他api也可以用, helper 偏domain, 只要是集合就可以, 改共用擴充方法, class 加static
    }
}