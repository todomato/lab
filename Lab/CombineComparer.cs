using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class CombineComparer<TKey> : IComparer<Employee>
    {
        public CombineComparer(Func<Employee, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            this.KeySelector = keySelector;
            this.KeyComparer = keyComparer;
        }

        public Func<Employee, TKey> KeySelector { get; private set; }
        public IComparer<TKey> KeyComparer { get; private set; }

        public int Compare(Employee employee, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(employee), KeySelector(minElement));
        }
    }
}