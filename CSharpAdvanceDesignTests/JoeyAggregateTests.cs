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

            var actual = JoeyAggregate(drawlingList, balance);

            var expected = 10.91m;

            Assert.AreEqual(expected, actual);
        }

        private decimal JoeyAggregate(IEnumerable<int> drawlingList, decimal balance)
        {
            var drawEnumerator = drawlingList.GetEnumerator();
            decimal result = balance;
            while (drawEnumerator.MoveNext())
            {
                var current = drawEnumerator.Current;
                if (result - current > 0)
                {
                    result -= current;
                }
            }
            return result;
        }
    }
}