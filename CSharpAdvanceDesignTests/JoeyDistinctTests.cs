using System;
using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using Lab.Entities;

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

        [Test]
        public void distinct_employees()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyDistinct(employees);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyDistinct(IEnumerable<Employee> employees)
        {
            return new HashSet<Employee>(employees, new FullNameComparer());
        }

        internal class FullNameComparer : IEqualityComparer<Employee>
        {
            public bool Equals(Employee x, Employee y)
            {
                return (x.FirstName == y.FirstName && x.LastName == y.LastName);
            }

            public int GetHashCode(Employee obj)
            {
                // 取得比對後相等的唯一值
                // 匿名型別 也可用在測試比較類別裡面的幾個property
                // 效能問題 等變成瓶頸再考慮
                return new {obj.FirstName, obj.LastName}.GetHashCode();
            }
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