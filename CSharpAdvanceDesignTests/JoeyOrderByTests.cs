using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    //[Ignore("not yet")]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeySort(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        //[Test]
        //public void orderBy_lastName_and_firstname()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = employees.JoeySort(new ComboComparer(
        //        new CombineComparer<string>(x => x.LastName, Comparer<string>.Default), 
        //        new CombineComparer<string>(x => x.FirstName, Comparer<string>.Default)));

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        [Test]
        public void orderBy_lastName_and_firstname_and_age()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 10},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 13},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 15},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 14},
            };

            //var firstComboComparer = new ComboComparer(
            //    new CombineComparer<string>(x => x.LastName, Comparer<string>.Default),
            //    new CombineComparer<string>(x => x.FirstName, Comparer<string>.Default));

            //var secondComboComparer = new ComboComparer(
            //    firstComboComparer ,
            //    new CombineComparer<int>(x => x.Age, Comparer<int>.Default));

            //var actual = employees.JoeySort(secondComboComparer);

            var actual = employees
                .JoeyOrderBy(e => e.LastName)
                .JoeyThenBy(e => e.FirstName)
                .JoeyThenBy(e => e.Age);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 14},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 15},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 13},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 10},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        // issue 變彈型 但是外面呼叫端麻煩
        // 一般情境 幫忙呼叫端 建立builder 幫忙做comparer
        // 但出現了linq 
    }
}

