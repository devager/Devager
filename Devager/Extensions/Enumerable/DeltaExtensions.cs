namespace Devager.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    /// <summary>
    /// Adds extension methods to the IEnumerable{T} interface.
    /// </summary>
    public static class Enumerable
    {
        public static void ForEach(this IEnumerable collection, Action<object> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static void ForEachWithIndex(this IEnumerable collection, Action<object, int> action)
        {
            var i = 0;

            foreach (var item in collection)
            {
                action(item, i++);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            var items = collection.ToArray();
            var count = items.Count();

            for (var i = 0; i < count; i++)
            {
                if (items[i] == null)
                {
                    throw new NullReferenceException("The value of the item cannot be null.");
                }

                action(items[i]);
            }
        }

        public static void ForEachWithIndex<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            var items = collection.ToArray();
            var count = items.Count();

            for (var i = 0; i < count; i++)
            {
                action(items[i], i);
            }
        }

        public static void AddRange<T>(this IList<T> originalList, IEnumerable<T> copyList)
        {
            if (Object.ReferenceEquals(copyList, null)) return;
            foreach (var cur in copyList)
            {
                originalList.Add(cur);
            }
        }
    }
}
