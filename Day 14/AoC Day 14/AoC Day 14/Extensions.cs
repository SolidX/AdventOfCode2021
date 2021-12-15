using System.Collections.Generic;

namespace AoC_Day_14
{
    public static class Extensions
    {
        public static Dictionary<char, ulong> CharacterFrequencyMap(this string str)
        {
            var dict = new Dictionary<char, ulong>();
            
            foreach (var c in str)
            {
                if (dict.ContainsKey(c))
                    dict[c]++;
                else
                    dict.Add(c, 1uL);
            }

            return dict;
        }

        public static void AddOrIncrement<TKey>(this Dictionary<TKey, ulong> d, TKey key)
        {
            if (d.ContainsKey(key))
                d[key]++;
            else
                d.Add(key, 1uL);
        }

        public static void AddOrIncrement<TKey>(this Dictionary<TKey, ulong> d, TKey key, ulong amount)
        {
            if (d.ContainsKey(key))
                d[key] += amount;
            else
                d.Add(key, amount);
        }
    }
}
