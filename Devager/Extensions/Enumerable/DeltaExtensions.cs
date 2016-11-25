namespace Devager.Extensions.Enumerable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    /// <summary>
    /// Adds extension methods to the IEnumerable{T} interface.
    /// </summary>
    public static class EnumerableExtensions
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
            int i = 0;

            foreach (var item in collection)
            {
                action(item, i++);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            T[] items = collection.ToArray();
            int count = items.Count();

            for (int i = 0; i < count; i++)
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
            T[] items = collection.ToArray();
            int count = items.Count();

            for (int i = 0; i < count; i++)
            {
                action(items[i], i);
            }
        }

        public static void AddRange<T>(this IList<T> originalList, IEnumerable<T> copyList)
        {
            if (!Object.ReferenceEquals(copyList, null))
            {
                foreach (var cur in copyList)
                {
                    originalList.Add(cur);
                }
            }
        }
    }
}
