using UnityEngine;

namespace Coremera
{
    public class PerspectiveCopyOrthographic : MonoBehaviour
    {
        Camera _camera;
        
        [SerializeField] Camera _copyCamera;

        [SerializeField] float _targetDepth;

        Vector2Int _previousScreenSize;
        float _previousCameraSize;
        
        void Awake()
        {
            _camera = GetComponent<Camera>();
        }

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
            if(size == _previousScreenSize || Mathf.Abs(_copyCamera.orthographicSize - _previousCameraSize) < 0.001f) { return; }

            Vector3 targetPoint = _copyCamera.ScreenToWorldPoint(new Vector3(size.x / 2.0f, size.y, _targetDepth));
            _camera.fieldOfView = Vector3.Angle(transform.position, targetPoint) * 2.0f;
            
            _previousScreenSize = size;
            _previousCameraSize = _copyCamera.orthographicSize;
        }
    }
}
