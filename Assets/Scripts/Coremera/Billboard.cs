using UnityEngine;

namespace Coremera
{
    [ExecuteInEditMode]
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        [SerializeField] private Vector3 m_rotationOffset;

        private void Update()
        {
            if(!m_target) { return; }
            transform.LookAt(m_target);
            transform.Rotate(m_rotationOffset);
        }
    }
}
