/* Adapted from https://github.com/Bl4ckb0ne/delaunay-triangulation and https://github.com/vazgriz/DungeonGenerator

Copyright (c) 2015-2019 Simon Zeni (simonzeni@gmail.com)
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

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graph.Graph
{
    public class Triangulation
    {
        public List<Vertex> Vertices    { get; private set; }
        public List<Edge> Edges         { get; }
        public List<Triangle> Triangles { get; }

        private Triangulation(IEnumerable<Vertex> vertices)
        {
            Vertices =  new List<Vertex>(vertices);
            Edges =     new List<Edge>();
            Triangles = new List<Triangle>();
        }

        public static Triangulation Triangulate(IEnumerable<Vertex> vertices)
        {
            Triangulation triangulation = new(vertices);
            triangulation.Triangulate();
            return triangulation;
        }

        public static Triangulation Triangulate(IEnumerable<Vector2> points)
        {
            var vertices = points.Select(point => new Vertex(point));
            return Triangulate(vertices);
        }

        private void Triangulate()
        {
            float xMin = Vertices[0].Position.x;
            float yMin = Vertices[0].Position.y;
            float xMax = xMin;
            float yMax = yMin;

            foreach (Vertex vertex in Vertices)
            {
                if(vertex.Position.x < xMin) { xMin = vertex.Position.x; }
                if(vertex.Position.x > xMax) { xMax = vertex.Position.x; }
                if(vertex.Position.y < yMin) { yMin = vertex.Position.y; }
                if(vertex.Position.y > yMax) { yMax = vertex.Position.y; }
            }
            
            float dx = xMax - xMin;
            float dy = yMax - yMin;
            float deltaMax = Mathf.Max(dx, dy) * 2.0f;

            Vertex p1 = new(new Vector2(xMin - 1,          yMin - 1));
            Vertex p2 = new(new Vector2(xMin - 1,          yMax + deltaMax));
            Vertex p3 = new(new Vector2(xMin + deltaMax,   yMin - 1));
            
            Triangles.Add(new Triangle(p1, p2, p3));

            foreach (Vertex vertex in Vertices)
            {
                var polygon = new List<Edge>();

                foreach (Triangle t in Triangles.Where(t => t.CircumferenceContains(vertex.Position)))
                {
                    t.IsBad = true;
                    polygon.Add(new Edge(t.A, t.B));
                    polygon.Add(new Edge(t.B, t.C));
                    polygon.Add(new Edge(t.C, t.A));
                }
                
                Triangles.RemoveAll(t => t.IsBad);

                for(int i = 0; i < polygon.Count; i++)
                {
                    for(int j = i + 1; j < polygon.Count; j++)
                    {
                        if(!polygon[i].AlmostEqual(polygon[j])) { continue; }
                        polygon[i].IsBad = true;
                        polygon[j].IsBad = true;
                    }
                }
                
                polygon.RemoveAll(e => e.IsBad);

                foreach (Edge edge in polygon)
                {
                    Triangles.Add(new Triangle(edge.U, edge.V, vertex));
                }
            }
            
            Triangles.RemoveAll(t => t.Contains(p1.Position) || t.Contains(p2.Position) || t.Contains(p3.Position));
            
            var edgeSet = new HashSet<Edge>();

            foreach (Triangle t in Triangles)
            {
                Edge ab = new(t.A, t.B);
                Edge bc = new(t.B, t.C);
                Edge ca = new(t.C, t.A);

                if (edgeSet.Add(ab)) { Edges.Add(ab); }
                if (edgeSet.Add(bc)) { Edges.Add(bc); }
                if (edgeSet.Add(ca)) { Edges.Add(ca); }
            }
        }
    }
}
