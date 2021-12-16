using System;

namespace AoC_Day_12
{
    public class Vertex : IEquatable<Vertex>
    {
        public string Name { get; private set; }

        public bool MultiVisit { get; set; }

        public bool Visited { get; set; }

        public Vertex(string name)
        {
            Name = name;
            MultiVisit = false;
            Visited = false;
        }

        public bool Equals(Vertex b)
        {
            if (Object.ReferenceEquals(b, null)) return false;
            if (Object.ReferenceEquals(this, b)) return true;

            return Name.Equals(b.Name, StringComparison.Ordinal); //Intentionally Case Sensitive
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
