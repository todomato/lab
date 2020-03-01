using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public interface IMyOrderByEnumerable : IEnumerable<Employee>
    {
        IMyOrderByEnumerable Append(IComparer<Employee> combineComparer);
    }

    public class MyOrderByEnumerable : IMyOrderByEnumerable
    {
        private IComparer<Employee> _untialComparer;
        private IEnumerable<Employee> _source;

        public MyOrderByEnumerable(IEnumerable<Employee> employees, IComparer<Employee> combineComparer)
        {
            _source = employees;
            _untialComparer = combineComparer;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return JoeySort(_source, _untialComparer);
        }

        // !明確方式
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static IEnumerator<Employee> JoeySort(IEnumerable<Employee> employees,
            IComparer<Employee> comboComparer)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];

                    if (comboComparer.Compare(employee, minElement) < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        public IMyOrderByEnumerable Append(IComparer<Employee> combineComparer)
        {
            _untialComparer = new ComboComparer(_untialComparer, combineComparer);
            return this;
        }
    }
}