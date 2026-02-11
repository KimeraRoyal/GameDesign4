using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    public class LocationSelector : MonoBehaviour
    {
        LocationNode _currentSelection;

        public LocationNode CurrentSelection => _currentSelection;

        public UnityEvent<LocationNode> OnSelected;
        public UnityEvent OnDeselected;

        public void Select(LocationNode location)
        {
            if(_currentSelection == location) { return; }
            _currentSelection = location;
            
            if (_currentSelection)
            {
                OnSelected?.Invoke(_currentSelection);
            }
            else
            {
                OnDeselected?.Invoke();
            }
        }
    }
}
