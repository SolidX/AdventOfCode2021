using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_05
{
    public static class Extensions
    {
        public static IDictionary<string, int> GetOverlappingPointsDictionary(this IEnumerable<CoordinatePair> lines)
        {
            var overlap = new Dictionary<string, int>();

            foreach(var line in lines)
            {
                var points = line.GetPoints();
                foreach (var pt in points)
                {
                    if (overlap.ContainsKey(pt.ToString()))
                    {
                        overlap[pt.ToString()] += 1;
                    }
                    else
                        overlap.Add(pt.ToString(), 1);
                }
            }

            return overlap;
        }
    }
}
