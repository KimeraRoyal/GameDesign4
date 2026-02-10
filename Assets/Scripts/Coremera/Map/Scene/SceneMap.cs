using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Scene
{
    [RequireComponent(typeof(Map))]
    public class SceneMap : MonoBehaviour
    {
        Map _map;

        [SerializeField] SceneNode _nodePrefab;
        [SerializeField] SceneConnection _connectionPrefab;

        Dictionary<Node, SceneNode> _nodes;
        List<SceneNode> _nodeList;
        
        Dictionary<Connection, SceneConnection> _connections;
        List<SceneConnection> _connectionList;

        public UnityEvent OnGenerated;

        public IReadOnlyList<SceneNode> Nodes => _nodeList;
        public IReadOnlyList<SceneConnection> Connections => _connectionList;

        void Awake()
        {
            _map = GetComponent<Map>();
            _map.OnGenerated.AddListener(MapGenerated);

            _nodes = new Dictionary<Node, SceneNode>();
            _nodeList = new List<SceneNode>();
            
            _connections = new Dictionary<Connection, SceneConnection>();
            _connectionList = new List<SceneConnection>();
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
                _nodeList.Add(sceneNode);
            }

            foreach (Connection connection in _map.Connections)
            {
                SceneConnection sceneConnection = Instantiate(_connectionPrefab, transform);
                sceneConnection.Assign(connection);
                _connections.Add(connection, sceneConnection);
                _connectionList.Add(sceneConnection);
            }
            
            OnGenerated?.Invoke();
        }
    }
}