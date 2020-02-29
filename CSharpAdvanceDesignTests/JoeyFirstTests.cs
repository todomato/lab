using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using Lab;

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

            var girl = LinqExtensions.JoeyFirst(girls);
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

            TestDelegate action = () => LinqExtensions.JoeyFirst(girls);
            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void get_first_chen()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
            var employee = employees.JoeyFirst(x => x.LastName == "Chen");
            new Employee() { FirstName = "Joey", LastName = "Chen" }.ToExpectedObject().ShouldMatch(employee);
        }
    }
}