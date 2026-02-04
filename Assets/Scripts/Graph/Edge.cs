using System;

namespace Graph
{
    public class Edge : IEquatable<Edge>
    {
        public Vertex U { get; set; }
        public Vertex V { get; set; }
        public bool IsBad { get; set; }

        public Edge() { }

        public Edge(Vertex u, Vertex v)
        {
            U = u;
            V = v;
        }

        public static bool operator ==(Edge a, Edge b)
            => (a.U == b.U || a.U == b.V) &&
               (a.V == b.U || a.V == b.V);

        public static bool operator !=(Edge a, Edge b)
            => !(a == b);

        public override bool Equals(object obj)
            => obj is Edge e && this == e;

        public bool Equals(Edge other)
            => this == other;

        public override int GetHashCode()
            => U.GetHashCode() ^ V.GetHashCode();

        public bool AlmostEqual(Edge other)
            => (U.AlmostEqual(other.U) && V.AlmostEqual(other.V)) ||
               (U.AlmostEqual(other.V) && V.AlmostEqual(other.U));
    }
}
