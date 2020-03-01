using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyUnionTests
    {
        [Test]
        public void union_numbers()
        {
            var first = new[] { 1, 3, 5, 3};
            var second = new[] { 5, 3, 7 };

            var actual = JoeyUnion(first, second);
            var expected = new[] { 1, 3, 5, 7 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyUnion(IEnumerable<int> first, IEnumerable<int> second)
        {
            //X anti-pattern  addrange + distinct

            var hashSet = new HashSet<int>();
            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var current = firstEnumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
            var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                var current = secondEnumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }

            // 既有
            //var hashSet = new HashSet<int>(first);
            //var enumerator = second.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    var enumeratorCurrent = enumerator.Current;
            //    hashSet.Add(enumeratorCurrent);
            //}
            //return hashSet;
        }
    }
}