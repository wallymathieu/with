using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using With.Linq;
namespace With.Rubyfy
{
    public static class RubyfyExtensions
    {
        public static bool Even(this int self)
        {
            return self%2==0;
        }
        public static bool Odd(this int self)
        {
            return self%2!=0;
        }

        public static IEnumerable<TRet> Map<T, TRet>(this IEnumerable<T> self, Func<T, int, TRet> map)
        {
            return self.Select(map);
        }
        public static IEnumerable<TRet> Collect<T, TRet>(this IEnumerable<T> self, Func<T, int, TRet> map)
        {
            return self.Select(map);
        }


        public static IEnumerable<TRet> Map<T, TRet>(this IEnumerable<T> self, Func<T, TRet> map)
        {
            return self.Select(map);
        }
        public static IEnumerable<TRet> Collect<T, TRet>(this IEnumerable<T> self, Func<T, TRet> map)
        {
            return self.Select(map);
        }

        public static T Reduce<T>(this IEnumerable<T> self, Func<T, T, T> map)
        {
            return self.Aggregate(map);
        }

        public static T Reduce<T>(this IEnumerable<T> self, T initial, Func<T, T, T> map)
        {
            return self.Aggregate(initial, map);
        }

        public static T Inject<T>(this IEnumerable<T> self, Func<T, T, T> map)
        {
            return self.Aggregate(map);
        }

        public static T Inject<T>(this IEnumerable<T> self, T initial, Func<T, T, T> map)
        {
            return self.Aggregate(initial, map);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> self)
        {
            return !self.Any();
        }

        public static T Detect<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            return self.FirstOrDefault(predicate);
        }

        public static T Find<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            return self.FirstOrDefault(predicate);
        }

        public static TRet MapDetect<T, TRet>(this IEnumerable<T> collection, Func<T, TRet> func) where TRet : class
        {
            foreach (var value in collection)
            {
                TRet result;
                if ((result = func(value)) != null)
                {
                    return result;
                }
            }
            return null;
        }

        public static T[] ToA<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray();
        }

        public static object[] ToA(this IEnumerable enumerable)
        {
            return enumerable.Cast<Object>().ToArray();
        }

        public static List<T> ToL<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }

        public static List<object> ToL(this IEnumerable enumerable)
        {
            return enumerable.Cast<Object>().ToList();
        }

        public static IEnumerable<T> SortBy<T, TValue>(this IEnumerable<T> self, Func<T, TValue> sortby)
        {
            return self.OrderBy(sortby);
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> self)
        {
            return self.OrderBy(t => t);
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            return self.OrderBy(compare);
        }

        public static IEnumerable<T> Select<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return self.Where(predicate);
        }

        public static IEnumerable<T> FindAll<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return self.Where(predicate);
        }

        public static bool Any<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.Any(self,predicate);
        }

        public static bool All<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.All(self,predicate);
        }

        public static int Count<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.Count(self,predicate);
        }
        public static int Count<T>(this IEnumerable<T> self)
        {
            return Enumerable.Count(self);
        }
        public static int Count<T>(this IEnumerable<T> self, T value)
        {
            return Enumerable.Count(self, e=> e.Equals(value));
        }

        public static IEnumerable<T> CollectConcat<T>(this IEnumerable<T> self, Func<T, IEnumerable<T>> map)
        {
            return self.FlatMap(map);
        }
        public static IEnumerable<T> FlatMap<T>(this IEnumerable<T> self, Func<T, IEnumerable<T>> map)
        {
            return self.Map(map).Flatten<T>();
        }
        public static IEnumerable<T> CollectConcat<T>(this IEnumerable self, Func<Object, IEnumerable> map)
        {
            return self.FlatMap<T>(map);
        }
        public static IEnumerable<T> FlatMap<T>(this IEnumerable self, Func<Object, IEnumerable> map)
        {
            return self.Cast<Object>().Map(map).Flatten<T>();
        }

        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> self, int? n=null)
        {
            while (n==null || n-- >0)
            {
                foreach (var item in self)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Drop<T>(this IEnumerable<T> self, int count)
        {
            return Enumerable.Skip<T>(self, count);
        }
        public static IEnumerable<T> DropWhile<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.SkipWhile<T>(self, predicate);
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var elem in self)
            {
                action(elem);
            }
            return self;
        }
        public static IEnumerable<Tuple<T1,T2>> Each<T1,T2>(this IEnumerable<Tuple<T1,T2>> self, Action<T1,T2> action)
        {
            foreach (var elem in self)
            {
                action(elem.Item1, elem.Item2);
            }
            return self;
        }
        public static IEnumerable<IGrouping<T1,T2>> Each<T1,T2>(this IEnumerable<IGrouping<T1,T2>> self, Action<T1,IEnumerable<T2>> action)
        {
            foreach (var elem in self)
            {
                action(elem.Key, elem);
            }
            return self;
        }

        /*
        EachCons
        */

        /*
        each_entry?
        */

        public static IEnumerable<IEnumerable<T>> EachSlice<T>(this IEnumerable<T> self, int count, Action<IEnumerable<T>> slice)
        {
            return self.EachSlice(count).Each(slice);
        }
        public static IEnumerable<IEnumerable<T>> EachSlice<T>(this IEnumerable<T> self, int count)
        {
            var l = new List<T>();
            foreach (var elem in self)
            {
                l.Add(elem);
                if (l.Count >= count)
                {
                    yield return l.ToArray();
                    l.Clear();
                }
            }
            if (l.Count > 0)
            {
                yield return l.ToArray();
            }
        }

        public static int FindIndex<T>(this IEnumerable<T> self, T item)
        {
            return self.FindIndex(elem => item.Equals(elem));
        }
        public static int FindIndex<T>(this IList<T> self, T item)
        {
            return self.IndexOf(item);
        }
        public static int FindIndex<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            var index = 0;
            foreach (var item in self)
            {
                if (predicate(item))
                {
                    return index;
                }

                index++;
            }
            return -1;
        }

        public static T First<T>(this IEnumerable<T> self)
        {
            return Enumerable.First(self);
        }
        public static IEnumerable<T> Cast<T>(this IEnumerable self)
        {
            return Enumerable.Cast<T>(self);
        }

        public static bool Include<T>(this IEnumerable<T> self, T member)
        {
            return Enumerable.Contains(self, member);
        }
        public static bool Member<T>(this IEnumerable<T> self, T member)
        {
            return Enumerable.Contains(self, member);
        }

        public static bool None<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return ! Enumerable.Any(self, predicate);
        }
        public static bool None<T>(this IEnumerable<T> self)
        {
            return ! Enumerable.Any(self);
        }

        public static bool One<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.Count(self, predicate) == 1;
        }
        public static bool One<T>(this IEnumerable<T> self)
        {
            return Enumerable.Count(self) == 1;
        }

        public static Partition<T> Partition<T>(this IEnumerable<T> self, Func<T,bool> partition)
        {
            var groups = self.GroupBy(partition);
            var trueArray = groups.SingleOrDefault(g => g.Key.Equals(true));
            var falseArray = groups.SingleOrDefault(g => g.Key.Equals(false));
            return new With.Rubyfy.Partition<T>(trueArray.ToArray(), falseArray.ToArray());
        }

        public static IEnumerable<T> Reject<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return self.Where(elem=> ! predicate(elem));
        }

        /*SliceAfter*/

        /*SliceBefore*/

        /*SliceWhen*/

        public static IEnumerable<T> Take<T>(this IEnumerable<T> self, int count)
        {
            return Enumerable.Take(self,count);
        }

        public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.TakeWhile(self,predicate);
        }

        public static IDictionary<TKey,TValue> ToH<TKey,TValue>(this IEnumerable<KeyValuePair<TKey,TValue>> self)
        {
            return self.ToDictionary();
        }
        public static IDictionary<TKey,TValue> ToH<TKey,TValue>(this IEnumerable<Tuple<TKey,TValue>> self)
        {
            return self.ToDictionary();
        }
        public static IDictionary<T,T> ToH<T>(this IEnumerable<T[]> self)
        {
            return self.ToDictionary();
        }

        public static IDictionary<TKey,TValue> ToH<T, TKey,TValue>(this IEnumerable<T> self, Func<T,TKey> keySelector, Func<T,TValue> valueSelector)
        {
            return self.ToDictionary(keySelector, valueSelector);
        }
    }
}
