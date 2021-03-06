﻿using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyExceptTests
    {
        private IEnumerable<int> JoeyExcept(IEnumerable<int> first, IEnumerable<int> second)
        {
            var hashSet = new HashSet<int>(second);
            var enumerator = first.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }

        [Test]
        public void except_numbers()
        {
            var first = new[] {1, 3, 5, 7, 3};

            var second = new[] {7, 1, 4, 1};
            var actual = JoeyExcept(first, second);
            var expected = new[] {3, 5};

            //var actual = JoeyExcept(second, first);
            //var expected = new[] {4 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}