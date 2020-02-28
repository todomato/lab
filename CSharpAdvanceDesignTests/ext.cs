using System;
using System.Collections.Generic;

static internal class ext
{
    public static List<TSource> JoeySelectWithIndex<TSource>(IEnumerable<TSource> urls, Func<TSource, int, TSource> selector)
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