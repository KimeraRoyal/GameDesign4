using System.Collections.Generic;
using System.Linq;
using Graph.Graph;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map.Map
{
    public class NodeConnector : MonoBehaviour
    {
        public enum Mode
        {
            None,
            Triangulate,
            MinimumSpanningTree,
            Corridors
        }

        [SerializeField] Mode _mode = Mode.Corridors;
        [SerializeField] [Range(0.0f, 100.0f)] float _corridorChance = 15.0f;

        List<Vertex<MapNode>> _vertices;
        List<Edge> _edges;

        public IReadOnlyList<Vertex> Vertices => _vertices;
        public IReadOnlyList<Edge> Edges => _edges;
        
        public void Generate(IEnumerable<MapNode> nodes)
        {
            _vertices = new List<Vertex<MapNode>>();
            foreach (MapNode node in nodes)
            {
                _vertices.Add(new Vertex<MapNode>(node.Position, node));
            }
            
            if(_mode == Mode.None) { return; }
            
            Triangulation triangulation = Triangulation.Triangulate(_vertices);
            if (_corridorChance > 99.99f)
            {
                _edges = triangulation.Edges;
                return;
            }
            
            if(_mode == Mode.Triangulate) { return; }
            
            MinimumSpanningTree mst = MinimumSpanningTree.Find(triangulation.Edges, triangulation.Vertices[0]);
            _edges = mst.Edges;
            
            if(_mode == Mode.MinimumSpanningTree) { return; }
            
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