using System;
using UnityEngine;

namespace Graph
{
    public class Triangle : IEquatable<Triangle>
    {
        public Vertex A { get; set; }
        public Vertex B { get; set; }
        public Vertex C { get; set; }
        public bool IsBad { get; set; }
        
        public Triangle() { }

        public Triangle(Vertex a, Vertex b, Vertex c)
        {
            A = a;
            B = b;
            C = c;
        }

        public bool Contains(Vector2 point)
            => Vector2.Distance(point, A.Position) < 0.01f ||
               Vector2.Distance(point, B.Position) < 0.01f ||
               Vector2.Distance(point, C.Position) < 0.01f;
        
        public bool CircumferenceContains(Vector2 point)
        {
            Vector2 a = A.Position;
            Vector2 b = B.Position;
            Vector2 c = C.Position;

            float ab = a.sqrMagnitude;
            float cd = b.sqrMagnitude;
            float ef = c.sqrMagnitude;

            float xCirc = (ab * (c.y - b.y)     + cd * (a.y - c.y)  + ef * (b.y - a.y)) /
                          (a.x * (c.y - b.y)    + b.x * (a.y - c.y) + c.x * (b.y - a.y));
            float yCirc = (ab * (c.x - b.x)     + cd * (a.x - c.x)  + ef * (b.x - a.x)) /
                          (a.y * (c.x - b.x)    + b.y * (a.x - c.x) + c.y * (b.x - a.x));

            Vector2 circumference = new (xCirc / 2.0f, yCirc / 2.0f);
            float circRadius = Vector2.SqrMagnitude(a - circumference);
            float dist = Vector2.SqrMagnitude(point - circumference);
            return dist <= circRadius;
        }
        
        public static bool operator ==(Triangle a, Triangle b)
            => (a.A == b.A || a.A == b.B || a.A == b.C) &&
               (a.B == b.A || a.B == b.B || a.B == b.C) &&
               (a.C == b.A || a.C == b.B || a.C == b.C);

        public static bool operator !=(Triangle a, Triangle b)
            => !(a == b);

        public override bool Equals(object obj)
            => obj is Triangle t && this == t;

        public bool Equals(Triangle other)
            => this == other;

        public override int GetHashCode()
            => A.GetHashCode() ^ B.GetHashCode() ^ C.GetHashCode();
        
        public bool AlmostEqual(Triangle other)
            => (A.AlmostEqual(other.A) || A.AlmostEqual(other.B) || A.AlmostEqual(other.C)) &&
               (B.AlmostEqual(other.A) || B.AlmostEqual(other.B) || B.AlmostEqual(other.C)) &&
               (C.AlmostEqual(other.A) || C.AlmostEqual(other.B) || C.AlmostEqual(other.C));
    }
}
