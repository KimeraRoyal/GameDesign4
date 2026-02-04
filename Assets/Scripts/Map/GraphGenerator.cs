using System;
using System.Collections.Generic;
using System.Linq;
using Graph.Graph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map.Map
{
    public class GraphGenerator : MonoBehaviour
    {
        public enum DebugState
        {
            None,
            ShowTriangulation,
            ShowMst,
            ShowFinal
        }
        
        [SerializeField] List<Vector2> _points;
        [SerializeField] DebugState _debugState;

        [SerializeField] [Range(0.0f, 100.0f)] float _corridorChance = 15.0f;

        public List<Vector2> Points => _points;
        
        Triangulation _triangulation;
        MinimumSpanningTree _mst;
        List<Edge> _edges;

        void Start()
        {
            Generate();
        }

        public void Generate()
        {
            int id = 0;
            var vertices = _points.Select(point => new Vertex<Node>(point, new Node(point, id++)));
            
            _triangulation = Triangulation.Triangulate(vertices);
            if (_corridorChance > 99.99f)
            {
                _edges = new List<Edge>(_triangulation.Edges);
                return;
            }
            _mst = MinimumSpanningTree.Find(_triangulation.Edges, _triangulation.Vertices[0]);

            _edges = new List<Edge>(_mst.Edges);
            foreach (Edge excluded in _mst.Excluded.Where(_ => Random.Range(0.0f, 100.0f) < _corridorChance))
            {
                _edges.Add(excluded);
            }
        }

        void OnDrawGizmosSelected()
        {
            var edges = _debugState switch
            {
                DebugState.None => null,
                DebugState.ShowTriangulation => _triangulation?.Edges,
                DebugState.ShowMst => _mst?.Edges,
                DebugState.ShowFinal => _edges,
                _ => throw new ArgumentOutOfRangeException()
            };

            if(edges == null) { return; }
            
            Gizmos.color = Color.green;
            foreach (Edge edge in edges)
            {
                Gizmos.DrawLine(edge.U.Position, edge.V.Position);
            }
        }
    }
}