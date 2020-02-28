using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinqExtensions
    {
        public static List<TSource> JoeyWhere<TSource>(this List<TSource> sources, Func<TSource, bool> predicate)
        {
            var result = new List<TSource>();
            foreach (var item in sources)
            {
                // 不一樣的地方抽成參數, duplicate 壞味道 : 為了消除重複才用Func
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> selector)
        {
            var result = new List<TResult>();
            foreach (var source in urls)
            {
                result.Add(selector(source)); 
            }
            return result;
        }

        public static List<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            var result = new List<TSource>();
            var index = 0;
            foreach (var item in source)
            {
                // postfix template
                if (predicate(item, index))
                {
                    result.Add(item);
                }
                index++;
            }
            return result;
        }

        public static List<TSource> JoeySelectWithIndex<TSource>(this IEnumerable<TSource> urls, Func<TSource, int, TSource> selector)
        {
            var result = new List<TSource>();
            var index = 0;
            foreach (var item in urls)
            {
                result.Add(selector(item, index));
                index++;
            }

            return result;
        }
    }
}