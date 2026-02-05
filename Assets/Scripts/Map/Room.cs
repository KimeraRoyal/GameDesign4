using System.Collections.Generic;
using Map.Map;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(NodeConnector))]
    public abstract class Room : MonoBehaviour
    {
        NodeConnector _connector;
        
        [SerializeField] bool _drawBounds;
        [SerializeField] bool _drawNodes;
        
        [SerializeField] Vector2 _size;
        
        List<MapNode> _nodes;

        public Vector2 Size => _size;

        public List<MapNode> Nodes => _nodes;

        protected void Awake()
        {
            _connector = GetComponent<NodeConnector>();
            
            _nodes = new List<MapNode>();
        }

        [Button("Generate")]
        public void Generate()
        {
            Populate();
            _connector.Connect(_nodes);
        }
        
        protected abstract void Populate();

        protected virtual void OnDrawGizmosSelected()
        {
            DrawBoundGizmos();
            DrawNodeGizmos();
        }

        private void DrawBoundGizmos()
        {
            if (!_drawBounds) { return; }
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, _size);
        }

        private void DrawNodeGizmos()
        {
            if(!_drawNodes || _nodes == null) { return; }
            
            Gizmos.color = Color.red;
            foreach (MapNode node in _nodes)
            {
                Gizmos.DrawCube(node.Position, Vector3.one * 0.1f);
            }
        }
    }
}
