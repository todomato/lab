using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class Comparer : IComparer<Employee>
    {
        public Comparer(Func<Employee, string> KeySelector, IComparer<string> KeyComparer)
        {
            this.KeySelector = KeySelector;
            this.KeyComparer = KeyComparer;
        }

        public Func<Employee, string> KeySelector { get; private set; }
        public IComparer<string> KeyComparer { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            return KeyComparer.Compare(KeySelector(x), KeySelector(y));
        }
    }
}