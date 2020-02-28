﻿using System;
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
    }
}