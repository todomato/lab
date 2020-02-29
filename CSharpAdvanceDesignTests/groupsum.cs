using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class GroupSumTests
    {
        [Test]
        // 其實就是做分頁
        public void group_sum_of_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };

            //sum of all Saving of each group which 3 Account per group
            //var actual = MyGroupSum(accounts, 3, c => c.Saving);
            var actual = MyGroupSum_Paging(accounts, 3, c => c.Saving);

            var expected = new[] { 60, 150, 240, 210 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        // 其實就是做分頁
        public void group_sum_of_paging()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };

            //sum of all Saving of each group which 3 Account per group
            var actual = MyGroupSum_Paging(accounts, 4, c => c.Saving);

            //注意邊界值,加測試
            var expected = new[] { 100, 260, 300 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        // 其實就是做分頁
        public void group_sum_of_one_paging()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };

            //sum of all Saving of each group which 3 Account per group
            var actual = MyGroupSum_Paging(accounts, 11, c => c.Saving);

            //注意邊界值,加測試
            var expected = new[] { 660 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static IEnumerable<int> MyGroupSum<TSource>(IEnumerable<TSource> source, int groupCount, Func<TSource, int> getValue)
        {
            var enumerator = source.GetEnumerator();
            var result = new List<int>();
            var index = 0;
            var sum = 0;
            var lastGroupIndex = groupCount - 1;
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                sum += getValue(item);

                if (index % groupCount == lastGroupIndex)
                {
                    result.Add(sum);
                    sum = 0;
                }

                index++;
            }

            // 如果遇到最後一頁數值為0 就會有問題
            // 可以用flag處理, 用true false做判斷即可,讓人容易理解 
            // 而不適用 != 0 意義不同
            // 比較大小 使用>, < 不會用 == 1
            // TODO bug
            if (sum != 0)
            {
                result.Add(sum);
            }

            return result;
        }


        private IEnumerable<int> MyGroupSum_Paging<TSource>(IEnumerable<TSource> source, int pageSize, Func<TSource, int> selector)
        {
            // 關注取得第一個 先確認第一群內容 take sum
            // 91版
            var list = source.ToList();
            var pageIndex = 0;
            while (pageIndex * pageSize < list.Count)
            {
                yield return list.Skip(pageIndex * pageSize).Take(pageSize).Sum(selector);
                pageIndex++;
            }

            //var enumerator = source.GetEnumerator();
            //var result = new List<int>();
            //var idx = 0;

            //while (enumerator.MoveNext())
            //{
            //    if (idx % groupCount == 0)
            //    {
            //        var sum = source
            //            .JoeySkip(idx)
            //            .JoeyTake(groupCount)
            //            .JoeySum(getValue);
            //        result.Add(sum);
            //    }

            //    idx++;
            //}

            //return result;
        }

        [Test]
        public void sum_of_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };

            //sum of all Saving of each group which 3 Account per group
            var actual = accounts.JoeySum(c => c.Saving);

            var expected = 660;
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void sum_of_top_3_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };

            //sum of all Saving of each group which 3 Account per group
            var result = accounts.JoeyTake(3).JoeySum(c => c.Saving);
            var actual = result;

            var expected = 60;

            expected.ToExpectedObject().ShouldMatch(actual);
        }

    }

    public class Account
    {
        public int Saving { get; set; }
        public string Name { get; set; }
    }
}