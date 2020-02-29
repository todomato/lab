using System;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    //[Ignore("not yet")]
    [TestFixture]
    public class JoeyAllTests
    {
        [Test]
        //用範例去確定疑惑 ex.18 or 20
        //public 方法要讓解釋跟寫的一樣
        public void girls_all_adult()
        {
            var girls = new List<Girl>
            {
                new Girl{Age = 20},
                new Girl{Age = 21},
                new Girl{Age = 17},
                new Girl{Age = 18},
                new Girl{Age = 30},
            };

            var actual = girls.JoeyAll(current => current.Age > 18);
            Assert.IsFalse(actual);
        }

        [Test]
        //用範例去確定疑惑 ex.18 or 20
        //public 方法要讓解釋跟寫的一樣
        public void girls_all_true_adult()
        {
            var girls = new List<Girl>
            {
                new Girl{Age = 20},
                new Girl{Age = 21},
                new Girl{Age = 19},
                new Girl{Age = 32},
                new Girl{Age = 30},
            };

            var actual = girls.JoeyAll(x => x.Age > 18);
            Assert.IsTrue(actual);
        }
    }
}