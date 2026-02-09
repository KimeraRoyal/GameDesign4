using UnityEngine;
using UnityEngine.Events;

namespace Coremera.Interaction.Mouse
{
    public class Mouse2D : MouseWorld
    {
        public UnityEvent<Vector2> OnMouseClickedWorld;
        
        protected override void ClickWorld(Vector3 _mouseWorldPos)
        {
            OnMouseClickedWorld?.Invoke(_mouseWorldPos);
        }

        protected override Transform Cast(Vector3 _mousePos)
        {
            var ray = Camera.ScreenPointToRay(_mousePos);
            var rayHit = Physics2D.Raycast(ray.origin, Vector2.up, 0.001f, m_targetMask);
            
            return rayHit.transform;
        }
    }
}