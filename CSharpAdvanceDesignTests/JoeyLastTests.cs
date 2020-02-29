using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    //alt + enter + new  ==> create class

    [TestFixture]
    public class JoeyLastTests 
    {
        [Test]
        public void get_last_chen()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Cash", LastName = "Li"},
            };

            //var employee = JoeyLast(employees);
            var employee = JoeyLast_v2(employees);

            new Employee { FirstName = "Cash", LastName = "Li" }
                .ToExpectedObject().ShouldMatch(employee);
        }

        [Test]
        public void get_last_employee_when_no_match()
        {
            var employees = new Employee[]
            {
            };

            TestDelegate action = () => JoeyLast(employees);
            Assert.Throws<InvalidOperationException>(action);
        }

        private Employee JoeyLast(IEnumerable<Employee> employees)
        {
            var enumerator = employees.GetEnumerator();
            Employee employee = null;
            while (enumerator.MoveNext())
            {
                employee = enumerator.Current;
            }
            return employee ?? throw new InvalidOperationException($"{nameof(employees)} is empty");
        }

        private Employee JoeyLast_v2(IEnumerable<Employee> employees)
        {
            var enumerator = employees.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            var last = enumerator.Current;
            while (enumerator.MoveNext())
            {
                last = enumerator.Current;
            }

            return last;
        }

    }
}