using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineComparer : IComparer<Employee>
    {
        public CombineComparer(Func<Employee, string> KeySelector, IComparer<string> KeyComparer)
        {
            this.KeySelector = KeySelector;
            this.KeyComparer = KeyComparer;
        }

        public Func<Employee, string> KeySelector { get; private set; }
        public IComparer<string> KeyComparer { get; private set; }

        public int Compare(Employee employee, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(employee), KeySelector(minElement));
        }
    }

    public class ComboComparer
    {
        public ComboComparer(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<Employee> FirstComparer { get; private set; }
        public IComparer<Employee> SecondComparer { get; private set; }
    }

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

        //    var actual = JoeyOrderByLastName(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        [Test]
        public void orderBy_lastName_and_firstname()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyOrderByLastName(employees, 
                new ComboComparer(
                    new CombineComparer(x => x.LastName, Comparer<string>.Default), 
                    new CombineComparer(x => x.FirstName, Comparer<string>.Default)));

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastName(
            IEnumerable<Employee> employees, 
            ComboComparer comboComparer)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];

                    var finalComboResult = 0;
                    var firstComparerResult = comboComparer.FirstComparer.Compare(employee, minElement);

                    if (firstComparerResult < 0)
                    {
                        finalComboResult = firstComparerResult;
                        //minElement = employee;
                        //index = i;
                    }
                    else if (firstComparerResult == 0)
                    {
                        var secondCompareResult = comboComparer.SecondComparer.Compare(employee, minElement);
                        if (secondCompareResult < 0)
                        {
                            finalComboResult = secondCompareResult;
                            //minElement = employee;
                            //index = i;
                        }
                    }

                    if (finalComboResult < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }

                   
        }

        // make method non static 練習轉打
    }
}

