using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = Distinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> Distinct(IEnumerable<int> numbers)
        {
            // 自己去重複 的資料結構特性
            return new HashSet<int>(numbers);

            //andy 
            var result = new List<int>();
            foreach (var item in numbers)
            {
                if (result.IndexOf(item) == -1)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}