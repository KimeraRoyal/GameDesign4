using UnityEngine;

namespace Coremera
{
    [ExecuteInEditMode]
    public class AlwaysFaceDirection : MonoBehaviour
    {
        [SerializeField] private Vector3 m_direction;

        private void Update()
        {
            transform.forward = m_direction.normalized;
        }
    }
}
