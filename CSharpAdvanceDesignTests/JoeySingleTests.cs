using NUnit.Framework;
using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySingleTests
    {
        [Test]
        public void no_girls()
        {
            var girls = new Girl[] { };
            TestDelegate action = () => JoeySingle(girls);
            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void only_one_girl()
        {
            var girls = new Girl[]
            {
                new Girl() {Name = "May"},
            };
            var girl = JoeySingle(girls);

            new Girl() { Name = "May" }.ToExpectedObject().ShouldMatch(girl);
        }

        [Test]
        public void more_than_one_girl()
        {
            var girls = new Girl[]
            {
                new Girl() {Name = "May"},
                new Girl() {Name = "Jessica"},
            };
            TestDelegate action = () => JoeySingle(girls);
            Assert.Throws<InvalidOperationException>(action);
        }

        private Girl JoeySingle(IEnumerable<Girl> girls)
        {
            //single 只有一筆 就可以用movenext 去判斷
            //觀念就要跳到不用foreach
            var enumerator = girls.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            var girl = enumerator.Current;
            if (enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            return girl;
        }
    }
}