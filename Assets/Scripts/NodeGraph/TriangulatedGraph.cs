using System.Collections.Generic;
using System.Linq;
using Graph;
using UnityEngine;

namespace NodeGraph
{
    public class TriangulatedGraph : MonoBehaviour
    {
        public class Node
        {
            public Vector2 Position { get; set; }
            public int ID { get; set; }

            public Node(Vector2 position, int id)
            {
                Position = position;
                ID = id;
            }
        }
        
        [SerializeField] List<Vector2> _points;

        public List<Vector2> Points => _points;
        
        Triangulation _triangulation;

        public void Triangulate()
        {
            int id = 0;
            var vertices = _points.Select(point => new Vertex<Node>(point, new Node(point, id++)));
            
            _triangulation = Triangulation.Triangulate(vertices);
        }

        void OnDrawGizmosSelected()
        {
            if(_triangulation == null) { return; }

            Gizmos.color = Color.green;
            foreach (Edge edge in _triangulation.Edges)
            {
                Gizmos.DrawLine(edge.U.Position, edge.V.Position);
            }
        }
    }
}