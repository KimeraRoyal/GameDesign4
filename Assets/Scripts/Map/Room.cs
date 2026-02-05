using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    public class Room : MonoBehaviour
    {
        [SerializeField] Vector2 _size;

        [SerializeField] uint _iterations = 4;
        
        [SerializeField] [MinMaxSlider(0.0f, 1.0f)] private Vector2 _splitRange = new(0.45f, 0.55f);

        [SerializeField] Vector2 _minSubdivisionSize;

        [SerializeField] Vector2 _border;
        [SerializeField] Vector2 _subdivisionPadding;

        RectDivider _rectDivider;

        [Button("Divide")]
        public void Divide()
        {
            Rect bounds = new((Vector2) transform.position - _size / 2.0f, _size);
            _rectDivider = RectDivider.Divide(bounds, _iterations, _splitRange, _minSubdivisionSize, _border, _subdivisionPadding);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, _size);
            
            if(_rectDivider == null) { return; }
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_rectDivider.PaddedBounds.center, _rectDivider.PaddedBounds.size);
            
            Gizmos.color = Color.green;
            foreach (Rect division in _rectDivider.Divisions)
            {
                Gizmos.DrawWireCube(division.center, division.size);
            }
        }
    }
}
