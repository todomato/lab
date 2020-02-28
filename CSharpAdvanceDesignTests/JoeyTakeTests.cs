using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeyTakeTests
    {
        [Test]
        public void take_2_employees()
        {
            var employees = (IEnumerable<Employee>) new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };

            // 取前兩筆
            var actual = employees.JoeyTake(2);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void take_3_employees()
        {
            var employees = (IEnumerable<Employee>)new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };

            // 取前兩筆
            var actual = employees.JoeyTake(3);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void take_4_names()
        {
            var names = new[] {"TOM", "Joey", "David"};

            // 取前兩筆
            var actual = names.JoeyTake(4);

            var expected = new[] { "TOM", "Joey", "David" };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        //一開始就用別人的用的角度去開發 -> TDD
        //一般人都會預先設計 就沒有人有能力去重購
        //重購 >> 重寫 功力阿
        //書單遺留代碼的藝術 , refactor, refactor to pattern
    }
}