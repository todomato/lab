using System;
using System.CodeDom;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            //TODO 可以轉打自己多參數的那隻,減少重複
            var enumerator = sources.GetEnumerator();

            // 延遲執行 會傳ienumerable + yield
            while (enumerator.MoveNext())
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

        public static IEnumerable<TSoruce> JoeySkip<TSoruce>(this IEnumerable<TSoruce> source, int count)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index >= count)
                {
                    yield return enumerator.Current;
                }

                index++;
            }
        }

        public static IEnumerable<Tsource> JoeySkip<Tsource>(this IEnumerable<Tsource> cards, Func<Tsource, bool> predicate)
        {
            var enumerator = cards.GetEnumerator();
            var isStartTaking = false;
            while (enumerator.MoveNext())
            {
                var card = enumerator.Current;
                //if (predicate(card) && !isStartTaking) continue;
                if (!predicate(card) || isStartTaking)
                {
                    isStartTaking = true;
                    yield return enumerator.Current;
                }
            }
        }

        public static int JoeySum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> value)
        {
            var enumerator = source.GetEnumerator();
            var sum = 0;
            while (enumerator.MoveNext())
            {
                var account = enumerator.Current;
                sum += value(account);
            }

            return sum;
        }

        public static bool JoeyAny(this IEnumerable<int> numbers, Func<int, bool> predicate)
        {
            var enumerator = numbers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return true;
                }
            }
            return  false;
        }

        public static bool JoeyAny(this IEnumerable<Employee> employees)
        {
            return employees.GetEnumerator().MoveNext();
        }

        public static bool JoeyAll(this IEnumerable<Girl> girls, Func<Girl, bool> predicate)
        {
            var enumerator = girls.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                // 在流程下code 要語意化
                if (!(predicate(current)))
                {
                    return false;
                }   
            }

            return true;
        }

        public static TSource JoeyFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    return current;
                }
            }
            throw new InvalidOperationException($"{nameof(source)} is empty");
        }

        public static TSource JoeyFirst<TSource>(IEnumerable<TSource> source)
        {
            var enumerator = source.GetEnumerator();
            //遇到 var return 這種是沒有意義的,爾且會有生命週期
            //可以改用function,就不會有生命週期

            // while 可以先寫,但如果一進去就return 就可以依序把while 改成 if,因為只有一次 
            return enumerator.MoveNext()
                ? enumerator.Current
                : throw new InvalidOperationException($"{nameof(source)} is empty");
        }
    }
}