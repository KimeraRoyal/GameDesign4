using UnityEngine;

namespace Input
{
    [RequireComponent(typeof(MouseInput))]
    public class DragMovement : MonoBehaviour
    {
        MouseInput _mouse;

        [SerializeField] Vector2 _dragAmount = Vector2.one;
        [SerializeField] float _dragDamping = 0.1f;
        
        Vector2 _dragVelocity;

        void Awake()
        {
            _mouse = GetComponent<MouseInput>();
            _mouse.OnSwipe.AddListener(Swipe);
        }

        void Swipe(Vector2 delta)
        {
            transform.localPosition += (Vector3) (delta * _dragAmount);
        }
    }
}