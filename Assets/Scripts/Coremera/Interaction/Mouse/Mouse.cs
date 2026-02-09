using UnityEngine;
using UnityEngine.Events;

namespace Coremera.Interaction.Mouse
{
    public abstract class Mouse : MonoBehaviour
    {
        [SerializeField] protected LayerMask m_targetMask;

        public UnityEvent<ClickableObject> OnObjectClicked;

        private int m_lock;

        public bool Locked => m_lock > 0;
        
        public UnityEvent<Vector2Int> OnMouseClicked;

        public void Lock()
            => m_lock++;

        public void Unlock()
            => m_lock--;

        protected virtual void Update()
        {
            if (Locked || !Input.GetMouseButtonDown(0)) { return; }

            var mousePos = Input.mousePosition;
            Click(mousePos);
            OnMouseClicked?.Invoke(new Vector2Int((int) mousePos.x, (int) mousePos.y));
        }

        protected virtual void Click(Vector2 _mousePos)
        {
            var hitTransform = Cast(_mousePos);
            if(!hitTransform) { return; }
            
            var clickableObject = hitTransform.GetComponentInParent<ClickableObject>();
            if(!clickableObject) { return; }
            
            clickableObject.Click();
            OnObjectClicked?.Invoke(clickableObject);
        }
        
        protected abstract Transform Cast(Vector3 _mousePos);
    }
}
