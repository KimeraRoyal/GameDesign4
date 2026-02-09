using System.Collections.Generic;
using UnityEngine;

namespace Map.Scene
{
    [RequireComponent(typeof(Map))]
    public class SceneMap : MonoBehaviour
    {
        Map _map;

        [SerializeField] SceneNode _nodePrefab;
        [SerializeField] SceneConnection _connectionPrefab;

        Dictionary<Node, SceneNode> _nodes;
        Dictionary<Connection, SceneConnection> _connections;

        void Awake()
        {
            _map = GetComponent<Map>();
            _map.OnGenerated.AddListener(MapGenerated);

            _nodes = new Dictionary<Node, SceneNode>();
            _connections = new Dictionary<Connection, SceneConnection>();
        }

        public SceneNode GetNode(Node node)
            => _nodes.GetValueOrDefault(node);

        public SceneConnection GetConnection(Connection connection)
            => _connections.GetValueOrDefault(connection);

        void MapGenerated()
        {
            foreach (Node node in _map.Nodes)
            {
                SceneNode sceneNode = Instantiate(_nodePrefab, transform);
                sceneNode.Assign(node);
                _nodes.Add(node, sceneNode);
            }

            foreach (Connection connection in _map.Connections)
            {
                SceneConnection sceneConnection = Instantiate(_connectionPrefab, transform);
                sceneConnection.Assign(connection);
                _connections.Add(connection, sceneConnection);
            }
        }
    }
}