﻿using System;
using System.CodeDom;
using System.Collections.Generic;

namespace Lab
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            var enumerator = sources.GetEnumerator();

            // 延遲執行 會傳ienumerable + yield
            while (enumerator.MoveNext() == true)
            {
                if (predicate(enumerator.Current))
                {
                    yield return enumerator.Current; //yield 可以記住自己的位置
                }
            }

            //var result = new List<TSource>();
            //foreach (var item in sources)
            //{
            //    // 不一樣的地方抽成參數, duplicate 壞味道 : 為了消除重複才用Func
            //    if (predicate(item))
            //    {
            //        result.Add(item);
            //    }
            //}
            //return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> selector)
        {
            var enumerator = urls.GetEnumerator();

            while (enumerator.MoveNext() == true)
            {
                yield return selector(enumerator.Current); //yield 可以記住自己的位置
            }

            //var result = new List<TResult>();
            //foreach (var source in urls)
            //{
            //    result.Add(selector(source)); 
            //}
            //return result;
        }

        // list 沒有藥用這麼大的資料結構 改用ienumerable
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                // postfix template
                if (predicate(enumerator.Current, index))
                {
                    yield return enumerator.Current;
                }
                index++;
            }


            //var result = new List<TSource>();
            //var index = 0;
            //foreach (var item in source)
            //{
            //    // postfix template
            //    if (predicate(item, index))
            //    {
            //        result.Add(item);
            //    }
            //    index++;
            //}
            //return result;
        }

        public static IEnumerable<TSource> JoeySelect<TSource>(this IEnumerable<TSource> urls, Func<TSource, int, TSource> selector)
        {
            var enumerator = urls.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                // postfix template
                yield return selector(enumerator.Current, index);
                index++;
            }

            //var result = new List<TSource>();
            //var index = 0;
            //foreach (var item in urls)
            //{
            //    result.Add(selector(item, index));
            //    index++;
            //}

            //return result;
        }

        // 非延遲執行
        // where select 跑8次+2次
        // 延遲執行
        // where 最多就8次 可以把多個方法組在同一個iterator裡面 簡單卻很重要

        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> employees, int count)
        {
            var enumerator = employees.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    yield return enumerator.Current;
                }
                else
                {
                    yield break;    //沒有值了
                }

                index++;
            }
        }
    }
}