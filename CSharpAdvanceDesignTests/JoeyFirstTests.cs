using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyFirstTests
    {
        [Test]
        public void get_first_girl()
        {
            var girls = new[]
            {
                new Girl(){Age = 60},
                new Girl(){Age = 20},
                new Girl(){Age = 30},
            };

            var girl = JoeyFirst(girls);
            var expected = new Girl { Age = 60 };

            expected.ToExpectedObject().ShouldEqual(girl);
        }

        [Test]
        // 應該要有設計有例外狀況
        public void get_first_girl_when_no_girls()
        {
            var girls = new Girl[]
            {
            };

            TestDelegate action = () => JoeyFirst(girls);
            Assert.Throws<InvalidOperationException>(action);
        }

        private TSource JoeyFirst<TSource>(IEnumerable<TSource> girls)
        {
            var enumerator = girls.GetEnumerator();
            //遇到 var return 這種是沒有意義的,爾且會有生命週期
            //可以改用function,就不會有生命週期

            // while 可以先寫,但如果一進去就return 就可以依序把while 改成 if,因為只有一次 
            return enumerator.MoveNext()
                ? enumerator.Current
                : throw new InvalidOperationException($"{nameof(girls)} is empty");
        }
    }
}