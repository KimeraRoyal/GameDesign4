using Input;
using UnityEngine;

namespace Map
{
    public class TouchableLocation : MonoBehaviour, Touchable
    {
        LocationSelector _selector;
        
        LocationNode _location;

        void Awake()
        {
            _selector = FindAnyObjectByType<LocationSelector>();

            _location = GetComponent<LocationNode>();
        }

        public void Touch()
            => _selector.Select(_location);
    }
}