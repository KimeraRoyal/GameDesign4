using System;
using UnityEngine;

namespace Graph
{
    public class Vertex : IEquatable<Vertex>
    {
        public Vector2 Position { get; set; }
        
        public Vertex() { }

        public Vertex(Vector2 position)
        {
            Position = position;
        }
        
        public static bool operator ==(Vertex a, Vertex b)
            => a.Equals(b);

        public static bool operator !=(Vertex a, Vertex b)
            => !(a == b);

        public bool Equals(Vertex other)
            => Position == other?.Position;

        public override bool Equals(object obj)
            => obj is Vertex v && this == v;

        public override int GetHashCode()
            => Position.GetHashCode();

        public bool AlmostEqual(Vertex b)
            => Position.AlmostEqual(b.Position);
    }

    public class Vertex<T> : Vertex
    {
        public T VertexData { get; set; }
        
        public Vertex() { }
        
        public Vertex(Vector2 position)
            : base(position) { }

        public Vertex(Vector2 position, T vertexData)
            : base(position)
        {
            VertexData = vertexData;
        }

        public override int GetHashCode()
            => base.GetHashCode() ^ VertexData.GetHashCode();
    }
}
