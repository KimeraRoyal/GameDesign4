using UnityEngine;

namespace Map.Scene
{
    public class SceneNode : MonoBehaviour
    {
        Node _node;
        
        SceneMap _map;
        
        protected virtual void Awake()
        {
            _map = GetComponentInParent<SceneMap>();
        }

        public void Assign(Node node)
        {
            if(_node != null) { return; }

            _node = node;

            transform.position = node.Position;
        }
    }
}