using System;
using System.Collections.Generic;

static internal class lin
{
    public static IEnumerable<TResult> JoeySelect<TSource, TResult>(IEnumerable<TSource> urls, Func<TSource, TResult> selector)
    {
        var result = new List<TResult>();
        foreach (var source in urls)
        {
            result.Add(selector(source)); 
        }
        return result;
    }
}