using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(NodeConnector))]
    public abstract class Map : MonoBehaviour
    {
        NodeConnector _connector;
        
        [SerializeField] Vector2 _size;

        List<Node> _nodes;
        HashSet<Connection> _connections;

        public Vector2 Size => _size;

        public List<Node> Nodes => _nodes;

        protected virtual void Awake()
        {
            _connector = GetComponent<NodeConnector>();
            
            _nodes = new List<Node>();
        }

        protected virtual void Start()
        {
            Generate();
        }

        [Button("Generate")]
        public void Generate()
        {
            Populate();
            _connections = _connector.Connect(Nodes);
        }
        
        protected abstract void Populate();

        protected virtual void OnDrawGizmosSelected()
        {
            if (_connections == null) { return; }

            Gizmos.color = Color.yellow;
            foreach (Connection connection in _connections)
            {
                Gizmos.DrawLine(connection.A.Position, connection.B.Position);
            }
        }
    }
}
