using NUnit.Framework;
using System;
using System.Collections.Generic;
using ExpectedObjects;

namespace CSharpAdvanceDesignTests
{

    [TestFixture]
    //[Ignore("not yet")]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_equal4()
        {
            var first = new List<int>();
            var second = new List<int>();

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_equal2()
        {
            var first = new List<int> { 3, 2, 1, 0 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal3()
        {
            var first = new List<int> { 3, 2, 1};
            var second = new List<int> { 3, 2, 1 ,0};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }


        private bool JoeySequenceEqual(IEnumerable<int> first, IEnumerable<int> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (true)
            {
                var firstEnumeratorCurrent = firstEnumerator.MoveNext();
                var secondEnumeratorCurrent = secondEnumerator.MoveNext();
                if (firstEnumeratorCurrent != secondEnumeratorCurrent)
                {
                    return false;
                }

                if (firstEnumeratorCurrent == false)
                {
                    return true;
                }

                if (firstEnumerator.Current != secondEnumerator.Current)
                {
                    return false;
                }
            }
           
        }

      
    }
}