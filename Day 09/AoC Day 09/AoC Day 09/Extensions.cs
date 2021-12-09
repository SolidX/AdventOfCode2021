using System.Collections.Generic;

namespace AoC_Day_09
{
    public static class Extensions
    {
        public static int Product(this IEnumerable<int> enumerable)
        {
            var product = 1;
            foreach (var x in enumerable)
                product *= x;
            return product;
        }
    }
}
