using System;
using System.Collections.Generic;
using System.Linq;
using Graph.Graph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class NodeConnector : MonoBehaviour
    {
        public enum Mode
        {
            None,
            Triangulate,
            MinimumSpanningTree,
            Corridors,
            ConnectAll,
            ConnectAllMinimumSpanningTree,
            Line
        }

        [SerializeField] Mode _mode = Mode.Corridors;
        [SerializeField] [Range(0.0f, 100.0f)] float _corridorChance = 15.0f;

        public HashSet<Connection> Connect(IEnumerable<Node> nodes)
        {
            var connections = new HashSet<Connection>();
            
            var vertices = new List<Vertex<Node>>();
            var edges = FindEdges(nodes, vertices);

            // TODO: Could make Connection class and form a list of them
            foreach (Edge edge in edges)
            {
                var a = edge.U as Vertex<Node>;
                var b = edge.V as Vertex<Node>;
                if(a == null || b == null) { continue; }

                connections.Add(new Connection(a.VertexData, b.VertexData));
            }

            return connections;
        }

        private List<Edge> FindEdges(IEnumerable<Node> nodes, List<Vertex<Node>> vertices)
        {
            vertices.AddRange(nodes.Select(node => new Vertex<Node>(node.Position, node)));

            var edges = _mode switch
            {
                Mode.None => new List<Edge>(),
                Mode.Triangulate or Mode.MinimumSpanningTree or Mode.Corridors =>   Triangulation.Triangulate(vertices).Edges,
                Mode.ConnectAll or Mode.ConnectAllMinimumSpanningTree =>            ConnectAll.Connect(vertices).Edges,
                Mode.Line =>                                                        ConnectLine(vertices),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            if (_corridorChance > 99.99f || _mode is not (Mode.MinimumSpanningTree or Mode.ConnectAllMinimumSpanningTree or Mode.Corridors)) { return edges; }
            
            MinimumSpanningTree mst = MinimumSpanningTree.Find(edges, vertices[0]);
            edges = mst.Edges;
            
            if(_mode is not Mode.Corridors) { return edges; }

            edges.AddRange(mst.Excluded.Where(_ => Random.Range(0.0f, 100.0f) < _corridorChance));

            return edges;
        }

        private static List<Edge> ConnectLine(IEnumerable<Vertex> vertices)
        {
            var edges = new List<Edge>();

            Vertex previous = null;
            foreach (Vertex vertex in vertices)
            {
                if(previous != null) { edges.Add(new Edge(previous, vertex)); }
                previous = vertex;
            }
            return edges;
        }
    }
}