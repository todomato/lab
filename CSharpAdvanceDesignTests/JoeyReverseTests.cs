using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyReverseTests
    {
        [Test]
        public void reverse_employees()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };

            var actual = JoeyReverse(employees);

            var expected = new List<Employee>
            {
                new Employee(){FirstName = "David",LastName = "Wang"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "Joey",LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyReverse(IEnumerable<Employee> employees)
        {
            var target = employees.ToList();
            var result = new List<Employee>();
            var count = employees.Count();
            for (int i = count - 1; i >= 0; i--)
            {
                result.Add(target[i]);
            }

            return result;
        }
    }
}