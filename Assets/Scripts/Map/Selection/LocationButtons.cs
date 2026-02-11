using UnityEngine;

namespace Map.Selection
{
    public class LocationButtons : MonoBehaviour
    {
        LocationButton[] _buttons;

        LocationSelector _selector;

        [SerializeField] Sprite _demolishButtonSprite;
        [SerializeField] string _demolishButtonTooltip = "Demolish";

        Building _previousBuilding;
        bool _updatingInformation;

        LocationNode _watched;

        void Awake()
        {
            _buttons = GetComponentsInChildren<LocationButton>();
        
            _selector = FindAnyObjectByType<LocationSelector>();
            _selector.OnSelected.AddListener(LocationSelected);
            _selector.OnDeselected.AddListener(LocationDeselected);
        }

        void Start()
        {
            foreach (LocationButton button in _buttons)
            {
                button.Hide();
                button.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            if(!_updatingInformation || _selector.CurrentSelection.Building == _previousBuilding) { return; }
            _previousBuilding = _selector.CurrentSelection.Building;
            
            UpdateButtons();
        }

        void LocationSelected(LocationNode location)
        {
            _updatingInformation = location.Type.SupportsBuildings;
            
            _watched = location;
            _watched?.OnForceStateChange.AddListener(NodeForceStateChange);
            
            UpdateButtons();
        }

        void LocationDeselected()
        {
            _updatingInformation = false;
            
            _watched?.OnForceStateChange.RemoveListener(NodeForceStateChange);

            _watched = null;
        }

        void NodeForceStateChange()
        {
            _previousBuilding = _selector.CurrentSelection.Building;
            
            UpdateButtons();
        }

        void UpdateButtons()
        {
            if (_selector.CurrentSelection.Building)
            {
                _buttons[0].gameObject.SetActive(true);
                _buttons[0].Show(_selector.CurrentSelection, _demolishButtonSprite, new LocationDemolishAction());
                for (int i = 1; i < _buttons.Length; i++)
                {
                    _buttons[i].Hide();
                    _buttons[i].gameObject.SetActive(false);
                }
            }
            else
            {
                int buttonCount = _selector.CurrentSelection.Type.SupportedBuildings.Length;
                for (int i = 0; i < buttonCount && i < _buttons.Length; i++)
                {
                    _buttons[i].gameObject.SetActive(true);
                    _buttons[i].Show(_selector.CurrentSelection, _selector.CurrentSelection.Type.SupportedBuildings[i].ButtonSprite, new LocationBuildAction(i));
                }

                for (int i = buttonCount; i < _buttons.Length; i++)
                {
                    _buttons[i].Hide();
                    _buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }
}