using UnityEngine;

namespace Coremera
{
    public class ScaleToScreen : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        
        Vector2Int _previousSize;
        
        void Start()
        {
            Scale(new Vector2Int(Screen.width, Screen.height));
        }

        void Update()
        {
            Scale(new Vector2Int(Screen.width, Screen.height));
        }

        void Scale(Vector2Int size)
        {
            if(size == _previousSize) { return; }

            Vector2 scale = _camera.ScreenToWorldPoint(new Vector3(size.x, size.y, transform.position.z)) * 2.0f;
            transform.localScale = new Vector3(scale.x, scale.y, 1.0f);
            
            _previousSize = size;
        }
    }
}
