using UnityEngine;

namespace Map
{
    public class LocationSelector : MonoBehaviour
    {
        LocationNode _currentSelection;

        [SerializeField] Transform _popup;

        public void Select(LocationNode location)
        {
            if(_currentSelection == location) { return; }

            _currentSelection = location;

            if (_currentSelection)
            {
                _popup.gameObject.SetActive(true);
                _popup.position = _currentSelection.transform.position + Vector3.up;
            }
            else
            {
                _popup.gameObject.SetActive(false);
            }
        }
    }
}
