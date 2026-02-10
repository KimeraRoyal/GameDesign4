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
    public class Vertex : IEquatable<Vertex>
    {
        public Vector2 Position { get; set; }
        
        public Vertex() { }

        public Vertex(Vector2 position)
        {
            Position = position;
        }
        
        public static bool operator ==(Vertex a, Vertex b)
            =>  (a is null && b is null) ||
                (a is not null && b is not null && 
                 a.Equals(b));

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
    }
}
