namespace CustomLinqExtensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> WhereEx<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> SelectEx<TSource, TResult>(
            this IEnumerable<TSource> source, 
            Func<TSource, TResult> selector)
        {
            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<T> DistinctEx<T>(this IEnumerable<T> source)
        {
            var seen = new HashSet<T>();
            foreach (var item in source)
            {
                if (seen.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<IGrouping<TKey, TSource>> GroupByEx<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector) where TKey : notnull
        {
            var groups = new Dictionary<TKey, List<TSource>>();

            foreach (var item in source)
            {
                var key = keySelector(item);
                if (!groups.ContainsKey(key))
                {
                    groups[key] = new List<TSource>();
                }
                groups[key].Add(item);
            }

            foreach (var kvp in groups)
            {
                yield return new Grouping<TKey, TSource>(kvp.Key, kvp.Value);
            }
        }
    }

    public class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        private readonly List<TElement> _elements;

        public Grouping(TKey key, List<TElement> elements)
        {
            Key = key;
            _elements = elements;
        }

        public TKey Key { get; }

        public IEnumerator<TElement> GetEnumerator() => _elements.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
