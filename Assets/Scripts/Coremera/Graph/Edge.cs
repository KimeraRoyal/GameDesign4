/* Adapted from https://github.com/vazgriz/DungeonGenerator

Copyright (c) 2019 Ryan Vazquez

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System;
using UnityEngine;

namespace Graph.Graph
{
    public class Edge : IEquatable<Edge>
    {
        public Vertex U { get; set; }
        public Vertex V { get; set; }
        public float Distance { get; private set; }
        public bool IsBad { get; set; }

        public Edge() { }

        public Edge(Vertex u, Vertex v)
        {
            U = u;
            V = v;
            Distance = Vector2.Distance(u.Position, v.Position);
        }

        public static bool operator ==(Edge a, Edge b)
            => (a is null && b is null) ||
               (a is not null && b is not null && 
               (a.U == b.U || a.U == b.V) &&
               (a.V == b.U || a.V == b.V));

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
