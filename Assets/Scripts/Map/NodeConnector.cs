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

        [SerializeField] bool _drawLines;

        [SerializeField] Mode _mode = Mode.Corridors;
        [SerializeField] [Range(0.0f, 100.0f)] float _corridorChance = 15.0f;

        List<Vertex<MapNode>> _vertices;
        List<Edge> _edges;

        public IReadOnlyList<Vertex> Vertices => _vertices;
        public IReadOnlyList<Edge> Edges => _edges;
        
        public void Connect(IEnumerable<MapNode> nodes)
        {
            _vertices = new List<Vertex<MapNode>>();
            foreach (MapNode node in nodes)
            {
                _vertices.Add(new Vertex<MapNode>(node.Position, node));
            }
            _edges?.Clear();

            if(_mode == Mode.None) { return; }

            switch (_mode)
            {
                case Mode.Triangulate:
                case Mode.MinimumSpanningTree:
                case Mode.Corridors:
                {
                    Triangulation triangulation = Triangulation.Triangulate(_vertices);
                    _edges = triangulation.Edges;
                    break;
                }
                case Mode.ConnectAll:
                case Mode.ConnectAllMinimumSpanningTree:
                {
                    ConnectAll connectAll = ConnectAll.Connect(_vertices);
                    _edges = connectAll.Edges;
                    break;
                }
                case Mode.Line:
                {
                    _edges = new List<Edge>();
                    for (int i = 0; i < _vertices.Count - 1; i++)
                    {
                        _edges.Add(new Edge(_vertices[i], _vertices[i + 1]));
                    }
                    return;
                }
            }
            
            if (_corridorChance > 99.99f || _mode == Mode.Triangulate || _mode == Mode.ConnectAll) { return; }
            
            MinimumSpanningTree mst = MinimumSpanningTree.Find(_edges, _vertices[0]);
            _edges = mst.Edges;
            
            if(_mode == Mode.MinimumSpanningTree || _mode == Mode.ConnectAllMinimumSpanningTree) { return; }
            
            foreach (Edge excluded in mst.Excluded.Where(_ => Random.Range(0.0f, 100.0f) < _corridorChance))
            {
                _edges.Add(excluded);
            }
        }

        void OnDrawGizmosSelected()
        {
            if(!_drawLines || _edges == null) { return; }
            
            Gizmos.color = Color.green;
            foreach (Edge edge in _edges)
            {
                Gizmos.DrawLine(edge.U.Position, edge.V.Position);
            }
        }
    }
}