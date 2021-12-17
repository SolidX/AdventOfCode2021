using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_16
{
    public static class Extensions
    {
        public static void AddRange<T>(this Queue<T> q, IEnumerable<T> range)
        {
            foreach (var i in range)
                q.Enqueue(i);
        }

        public static List<T> DequeueRange<T>(this Queue<T> q, int size)
        {
            var output = new List<T>(size);
            for (var i = 0; i < size; i++)
                output.Add(q.Dequeue());
            return output;
        }

        public static byte ToByte(this IEnumerable<bool> bits)
        {
            var length = bits.Count();
            if (length > 8) throw new NotImplementedException();

            var idx = 8 - length;
            byte result = 0;

            foreach (var b in bits)
            {
                if (b)
                    result |= (byte)(1u << (7 - idx));
                idx++;
            }

            return result;
        }

        public static byte[] ToBytes(this IEnumerable<bool> bits)
        {
            var output = new List<byte>();
            
            var stream = new Queue<bool>(bits);
            if (stream.Count % 8 != 0)
            {
                var padding = Enumerable.Repeat(false, 8 - stream.Count % 8).ToList();
                padding.AddRange(stream);
                stream = new Queue<bool>(padding);
            }

            while (stream.Count > 0)
                output.Add(stream.DequeueRange(8).ToByte());
            
            return output.ToArray();
        }

        public static ulong ToUInt64(this IEnumerable<byte> bytes)
        {
            var result = 0uL;
            var stream = bytes.ToList();

            if (stream.Count > 8) throw new NotImplementedException(); //this is above my paygrade

            if (stream.Count < 8)
            {
                var padding = Enumerable.Repeat((byte)0, 8 - stream.Count).ToList();
                padding.AddRange(stream);
                stream = padding;
            }

            var idx = 1;
            foreach (var b in stream)
            {
                var tmp = Convert.ToUInt64(b);
                result += tmp << ((stream.Count - idx) * 8);
                idx++;
            }

            return result;
        }

        public static ulong Sum<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, ulong> selector)
        {
            var sum = 0uL;

            foreach (var i in enumerable)
                sum += selector(i);

            return sum;
        }

        public static ulong Product<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, ulong> selector)
        {
            var product = !enumerable.Any() ? 0uL : 1uL;

            foreach (var i in enumerable)
                product *= selector(i);

            return product;
        }
    }
}
