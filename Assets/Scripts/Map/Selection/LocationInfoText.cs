using System;
using TMPro;
using UnityEngine;

namespace Map.Selection
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocationInfoText : MonoBehaviour
    {
        public enum Type
        {
            Title,
            Information
        }
        
        TMP_Text _text;

        LocationSelector _selector;

        [SerializeField] Type _type;
    
        [SerializeField] string _format = "{0}";

        [SerializeField] float _updateInterval = 1.0f;
        float _updateTimer;
        bool _updatingInformation;

        LocationNode _watched;
    
        void Awake()
        {
            _text = GetComponent<TMP_Text>();
        
            _selector = FindAnyObjectByType<LocationSelector>();
            _selector.OnSelected.AddListener(LocationSelected);
            _selector.OnDeselected.AddListener(LocationDeselected);
        }

        void Update()
        {
            if(!_updatingInformation) { return; }
            
            _updateTimer += Time.deltaTime;
            if(_updateTimer < _updateInterval) { return; }
            _updateTimer -= _updateInterval;

            UpdateText();
        }

        void LocationSelected(LocationNode location)
        {
            _updatingInformation = location.Type.InformationUpdates;
            _updateTimer = 0.0f;

            _watched = location;
            _watched?.OnForceStateChange.AddListener(NodeForceStateChange);
            
            UpdateText();
        }

        void LocationDeselected()
        {
            _updatingInformation = false;
            
            _watched?.OnForceStateChange.RemoveListener(NodeForceStateChange);

            _watched = null;
        }

        void NodeForceStateChange()
        {
            _updateTimer = 0.0f;
            
            UpdateText();
        }

        void UpdateText()
        {
            LocationNode location = _selector.CurrentSelection;
            _text.text = _type switch
            {
                Type.Title => string.Format(_format, location.GetName(), location.Position.x, location.Position.y),
                Type.Information => string.Format(_format, location.GetInformation()),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
