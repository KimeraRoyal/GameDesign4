using UnityEngine;

namespace Coremera.Interaction.Mouse
{
    [RequireComponent(typeof(Camera))]
    public abstract class MouseWorld : Mouse
    {
        private Camera m_camera;

        protected Camera Camera => m_camera;
        
        private void Awake()
        {
            m_camera = GetComponent<Camera>();
        }
        
        protected override void Click(Vector2 _mousePos)
        {
            base.Click(_mousePos);
            ClickWorld(m_camera.ScreenToWorldPoint(_mousePos));
        }

        protected abstract void ClickWorld(Vector3 _mouseWorldPos);
    }
}