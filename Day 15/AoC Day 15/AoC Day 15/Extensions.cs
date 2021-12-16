using System.Collections.Generic;

namespace AoC_Day_15
{
    public static class Extensions
    {
        public static void InsertOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value)
        {
            if (d.ContainsKey(key))
                d[key] = value;
            else
                d.Add(key, value);
        }
    }
}
