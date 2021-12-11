namespace AoC_Day_11
{
    public static class Extensions
    {
        public static ushort[][] DeepClone(this ushort[][] source)
        {
            var target = new ushort[source.Length][];
            for (var i = 0; i < source.Length; i++)
            {
                target[i] = new ushort[source[i].Length];

                for (var j = 0; j < source[i].Length; j++)
                    target[i][j] = source[i][j];
            }

            return target;
        }
    }
}
