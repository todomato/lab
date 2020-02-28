using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    //[Ignore("not yet")]
    public class JoeySelectTests
    {
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls();

            var actual = JoeySelect(urls, item => item.Replace("http:", "https:"));
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void append_port_9191_url()
        {
            var urls = GetUrls();

            var actual = JoeySelect(urls, url => $"{url}:9191");
            var expected = new List<string>
            {
                "http://tw.yahoo.com:9191",
                "https://facebook.com:9191",
                "https://twitter.com:9191",
                "http://github.com:9191"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        //TODO 發現只有add參數不同, 提取參數

        private IEnumerable<TResult> JoeySelect<TSource, TResult>(IEnumerable<TSource> urls, Func<TSource, TResult> selector)
        {
            var result = new List<TResult>();
            foreach (var item in urls)
            {
                result.Add(selector(item)); 
            }
            return result;
        }

        private IEnumerable<string> JoeySelectForEmployee(List<Employee> employees, Func<Employee, string> selector)
        {
            return JoeySelect(employees, selector);
        }

        [Test]
        public void select_full_name()
        {
            //呼叫方法然後用inline 不用自己複製
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };

            var names = JoeySelectForEmployee(employees, e => $"{e.FirstName} {e.LastName}");
            var expected = new[]
            {
                "Joey Chen",
                "Tom Li",
                "David Chen"
            };

            expected.ToExpectedObject().ShouldMatch(names);
        }

    


        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
        }
    }
}