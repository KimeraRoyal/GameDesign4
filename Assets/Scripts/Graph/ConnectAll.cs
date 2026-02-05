using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graph.Graph
{
    public class ConnectAll
    {
        public List<Vertex> Vertices    { get; private set; }
        public List<Edge> Edges         { get; }

        private ConnectAll(IEnumerable<Vertex> vertices)
        {
            Vertices =  new List<Vertex>(vertices);
            Edges =     new List<Edge>();
        }

        public static ConnectAll Connect(IEnumerable<Vertex> vertices)
        {
            ConnectAll connectAll = new(vertices);
            connectAll.Connect();
            return connectAll;
        }

        public static ConnectAll Connect(IEnumerable<Vector2> points)
        {
            var vertices = points.Select(point => new Vertex(point));
            return Connect(vertices);
        }

        private void Connect()
        {
            for(int i = 0; i < Vertices.Count; i++)
            {
                for(int j = i + 1; j < Vertices.Count; j++)
                {
                    Edges.Add(new Edge(Vertices[i], Vertices[j]));
                }
            }
        }
    }
}