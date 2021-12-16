using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AoC_Day_12
{
    public class NodeEqualityComparer : IEqualityComparer<Vertex>
    {
        public bool Equals(Vertex x, Vertex y)
        {
            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] Vertex obj)
        {
            return obj.GetHashCode();
        }
    }
}
