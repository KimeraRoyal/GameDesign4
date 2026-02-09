using UnityEngine;
using UnityEngine.Events;

namespace Coremera.Interaction.Mouse
{
    public class Mouse3D : MouseWorld
    {
        public UnityEvent<Vector3> OnMouseClickedWorld;
        
        protected override void ClickWorld(Vector3 _mouseWorldPos)
        {
            OnMouseClickedWorld?.Invoke(_mouseWorldPos);
        }
        
        protected override Transform Cast(Vector3 _mousePos)
        {
            var ray = Camera.ScreenPointToRay(_mousePos);
            if (!Physics.Raycast(ray, out var rayHit, Camera.farClipPlane, m_targetMask)) { return null; }

            return rayHit.transform;
        }
    }
}
