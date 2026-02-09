using UnityEngine;

namespace Coremera.Interaction.Mouse
{
    [RequireComponent(typeof(UIMouseRaycaster))]
    public class MouseUI : Mouse
    {
        private UIMouseRaycaster m_raycaster;

        private void Awake()
        {
            m_raycaster = GetComponent<UIMouseRaycaster>();
        }

        protected override Transform Cast(Vector3 _mousePos)
        {
            var hitObject = m_raycaster.GetFirstRaycastObject();
            return hitObject ? hitObject.transform : null;
        }
    }
}