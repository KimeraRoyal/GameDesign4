using System;

namespace Map
{
    public class Connection : IEquatable<Connection>
    {
        public Node A { get; private set; }
        public Node B { get; private set; }

        public Connection(Node a, Node b)
        {
            A = a;
            B = b;
            
            a.Connect(b);
        }

        public static bool operator ==(Connection a, Connection b)
            => (a is null && b is null) ||
               ReferenceEquals(a, b) ||
               (a is not null && b is not null && 
                a.A == b.A && a.B == b.B);

        public static bool operator !=(Connection a, Connection b)
            => !(a == b);

        public bool Equals(Connection other)
            => this == other;

        public override bool Equals(object obj)
            => obj is Connection c && this == c;

        public override int GetHashCode()
            => A.GetHashCode() ^ B.GetHashCode();
    }
}