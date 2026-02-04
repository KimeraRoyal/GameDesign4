using System.Collections.Generic;
using System.Linq;
using Graph.Graph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map.Map
{
    public class MapGenerator : MonoBehaviour
    {
        static int _nodeId;
        
        [SerializeField] List<Vector2> _points;

        [SerializeField] [Range(0.0f, 100.0f)] float _corridorChance = 15.0f;

        List<Vertex<Node>> _vertices;
        List<Edge> _edges;

        public IReadOnlyList<Vertex> Vertices => _vertices;
        public IReadOnlyList<Edge> Edges => _edges;

        void Start()
        {
            Generate();
        }

        public void Generate()
        {
            _vertices = _points.Select(point => new Vertex<Node>(point, new Node(point, _nodeId++))).ToList();
            
            Triangulation triangulation = Triangulation.Triangulate(_vertices);
            if (_corridorChance > 99.99f)
            {
                _edges = triangulation.Edges;
                return;
            }
            MinimumSpanningTree mst = MinimumSpanningTree.Find(triangulation.Edges, triangulation.Vertices[0]);

            _edges = mst.Edges;
            foreach (Edge excluded in mst.Excluded.Where(_ => Random.Range(0.0f, 100.0f) < _corridorChance))
            {
                _edges.Add(excluded);
            }
        }

        void OnDrawGizmosSelected()
        {
            if(_edges == null) { return; }
            
            Gizmos.color = Color.green;
            foreach (Edge edge in _edges)
            {
                Gizmos.DrawLine(edge.U.Position, edge.V.Position);
            }
        }
    }
}