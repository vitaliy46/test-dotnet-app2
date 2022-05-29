using System;
using System.Collections.Generic;

namespace aiPeopleTracker.Business.Collections
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable != null)
            {
                foreach (var cur in enumerable)
                {
                    if (action != null)
                    {
                        action(cur);
                    }
                }
            }
        }
    }
}