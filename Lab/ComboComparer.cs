using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboComparer :　IComparer<Employee>
    {
        public ComboComparer(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<Employee> FirstComparer { get; private set; }
        public IComparer<Employee> SecondComparer { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            if (FirstComparer.Compare(x, y) != 0)
            {
                return FirstComparer.Compare(x, y);
            }

            return SecondComparer.Compare(x, y);
        }
    }
}