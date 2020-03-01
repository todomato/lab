using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee { FirstName = "David", LastName = "Chen" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Chen" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(
                employees, 
                pets, 
                employee => employee, 
                pet => pet.Owner, 
                pet1 => Tuple.Create(((Func<Pet, Employee>) (pet => pet.Owner))(pet1).FirstName, pet1.Name));

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu"),
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Tuple<string, string>> JoeyJoin(
            IEnumerable<Employee> employees, 
            IEnumerable<Pet> pets, 
            Func<Employee, Employee> outerSelector, 
            Func<Pet, Employee> innerSelector, 
            Func<Pet, Tuple<string, string>> resultSelector)
        {
            var enumerator = employees.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                var pertsEnumerator = pets.GetEnumerator();
                while (pertsEnumerator.MoveNext())
                {
                    var pet = pertsEnumerator.Current;
                    if (outerSelector(current) == innerSelector(pet))
                    {
                        yield return resultSelector(pet);
                    }
                }
            }

            // select manay
            //var enumerator = employees.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    var current = enumerator.Current;

            //    var pertsEnumerator = pets.GetEnumerator();
            //    while (pertsEnumerator.MoveNext())
            //    {
            //        var pet = pertsEnumerator.Current;
            //       yield return Tuple.Create(pet.Owner.FirstName , pet.Name);
            //    }
            //}   
        }
    }
}