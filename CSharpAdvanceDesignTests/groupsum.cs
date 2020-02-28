using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class GroupSumTests
    {
        [Test]
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
            var actual = MyGroupSum(accounts);

            var expected = new[] { 60, 150, 240, 210 };

            expected.ToExpectedObject().ShouldMatch(actual);
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

        private IEnumerable<int> MyGroupSum(IEnumerable<Account> accounts)
        {
            var enumerator = accounts.GetEnumerator();
            var result = new List<int>();
            var idx = 0;
            var groupCount = 3;

            while (enumerator.MoveNext())
            {
                if (idx % groupCount == 0)
                {
                    var sum = accounts.JoeySkip(idx).JoeyTake(groupCount).JoeySum(c => c.Saving);
                    result.Add(sum);
                }

                idx++;
            }

            return result;
        }
    }

    public class Account
    {
        public int Saving { get; set; }
        public string Name { get; set; }
    }
}