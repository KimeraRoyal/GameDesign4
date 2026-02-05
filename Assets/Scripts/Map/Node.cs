using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Node
    {
        public Vector2 Position { get; set; }

        List<Node> _connections;

        public IReadOnlyList<Node> Connections => _connections;

        public Node(Vector2 position)
        {
            Position = position;

            _connections = new List<Node>();
        }

        public void Connect(Node other)
        {
            if(_connections.Contains(other)) { return; }
            _connections.Add(other);
            other._connections.Add(this);
        }
    }
}