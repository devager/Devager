namespace Devager.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class MergeItem<TSource, TDest>
        where TSource : class
        where TDest : class
    {
        public TSource Source { get; set; }
        public TDest Dest { get; set; }

        public MergeItem(TSource source, TDest dest)
        {
            Source = source;
            Dest = dest;
        }

        public override string ToString()
        {
            return string.Format("Source:{0} Dest:{1}", Source, Dest);
        }
    }

    public class MergeDelta<TSource, TDest>
        where TSource : class
        where TDest : class
    {
        public IList<TSource> InsertedOnly { get; set; }
        public IList<TDest> DeletedOnly { get; set; }
        public IList<MergeItem<TSource, TDest>> Inserted { get; set; }
        public IList<MergeItem<TSource, TDest>> Updated { get; set; }
        public IList<MergeItem<TSource, TDest>> Deleted { get; set; }

        public override string ToString()
        {
            return string.Format("Insert:{0} Update:{1} Delete:{2}", Inserted.Count, Updated.Count, Deleted.Count);
        }
    }

    public static class Delta
    {
        public static MergeDelta<TSource, TDest> GetMergeDelta<TSource, TDest>(Dictionary<string, MergeItem<TSource, TDest>> mergedDictionary)
            where TSource : class
            where TDest : class
        {
            var result = new MergeDelta<TSource, TDest>();

            result.InsertedOnly = mergedDictionary.Where(d => d.Value.Dest == null).Select(d => d.Value.Source).ToList();
            result.DeletedOnly = mergedDictionary.Where(d => d.Value.Source == null).Select(d => d.Value.Dest).ToList();

            result.Inserted = mergedDictionary.Where(d => d.Value.Dest == null).Select(d => d.Value).ToList();
            result.Updated = mergedDictionary.Where(d => d.Value.Source != null && d.Value.Dest != null).Select(d => d.Value).ToList();
            result.Deleted = mergedDictionary.Where(d => d.Value.Source == null).Select(d => d.Value).ToList();

            return result;
        }

        public static Dictionary<string, MergeItem<TSource, TDest>> MergeDictionary<TSource, TDest>(
            IEnumerable<TSource> source, IEnumerable<TDest> destination,
            Func<TSource, string> sourceKey,
            Func<TDest, string> destKey
            )
            where TSource : class
            where TDest : class
        {
            // Add source to dictionary
            var dict = source.ToDictionary(sourceKey, s => new MergeItem<TSource, TDest>(s, null));

            // Add destination to dictionary
            foreach (var dest in destination)
            {
                if (dict.ContainsKey(destKey(dest)))
                {
                    // destination exists in source
                    dict[destKey(dest)].Dest = dest;
                }
                else
                {
                    // destination does not exist in source
                    dict.Add(destKey(dest), new MergeItem<TSource, TDest>(null, dest));
                }
            }

            return dict;
        }
    }
}