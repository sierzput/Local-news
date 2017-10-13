using System.Collections.Generic;
using System.Linq;

namespace LocalNews.Helpers
{
    public static class EnumerableExtensions
    {
        public static IList<T> Materialize<T>(this IEnumerable<T> queryable)
        {
            return queryable as IList<T> ?? queryable.ToList();
        }
    }
}
