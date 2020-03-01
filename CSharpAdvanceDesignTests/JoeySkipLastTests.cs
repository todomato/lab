using System;
using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySkipLastTests
    {
        [Test]
        public void skip_last_2()
        {
            var numbers = new[] { 10, 20, 30, 40, 50 };
            var actual = JoeySkipLast(numbers, 2);

            var expected = new[] { 10, 20, 30 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeySkipLast(IEnumerable<int> numbers, int count)
        {
            throw new NotImplementedException();
        }

    }
}