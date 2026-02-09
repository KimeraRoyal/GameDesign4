using UnityEngine;

namespace Coremera
{
    [ExecuteInEditMode]
    public class BillboardOnAxis : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        
        [SerializeField] private Vector3 m_axis;
        [SerializeField] private Vector3 m_rotationOffset;

        private void Update()
        {
            if(!m_target) { return; }

            var forward = (m_target.position - transform.position).normalized;
            for (var i = 0; i < 3; i++)
            {
                forward[i] = Mathf.Lerp(forward[i], Vector3.forward[i], m_axis[i]);
            }
            transform.forward = forward;
            transform.Rotate(m_rotationOffset);
        }
    }
}
