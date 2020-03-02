using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    //[Ignore("not yet")]
    [TestFixture]
    public class JoeyAggregateTests
    {
        [Test]
        public void drawling_money_that_balance_have_to_be_positive()
        {
            // test
            var balance = 100.91m;

            var drawlingList = new List<int>
            {
                30, 80, 20, 40, 25
            };

            var actual = JoeyAggregate(drawlingList, 
                balance,
                (total, next) => total - next > 0 ? total - next : total);

            var expected = 10.91m;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Determine_whether_any_string_in_the_array_is_longer_than_banana_()
        {
            // test
            string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };
            var banana = "banana";

            var actual = JoeyAggregate(
                fruits,
                banana,
                (longest, next) => (longest.Length > next.Length) ? longest: next);

            var expected = "passionfruit";
            Assert.AreEqual(expected, actual);
        }

        private TAccumate JoeyAggregate<TSource,TAccumate>(IEnumerable<TSource> source, TAccumate seed, Func<TAccumate, TSource, TAccumate> func)
        {
            var enumerator = source.GetEnumerator();
            var result = seed;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                result = func(result, current);
            }
            return result;
        }
    }
}