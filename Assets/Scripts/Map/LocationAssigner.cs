using System;
using Map.Scene;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    [RequireComponent(typeof(SceneMap))]
    public class LocationAssigner : MonoBehaviour
    {
        [Serializable]
        public class WeightedOption
        {
            [SerializeField] LocationType _type;
            
            [SerializeField] float _weight = 1.0f;
            [SerializeField] float _totalWeight = 0.0f;
            
            public LocationType Type => _type;

            public float Weight => _weight;

            public float TotalWeight
            {
                get => _totalWeight;
                set => _totalWeight = value;
            }
        }

        SceneMap _map;
        
        [SerializeField] WeightedOption[] _options;
        [SerializeField] float _totalWeight;

        void Awake()
        {
            _map = GetComponent<SceneMap>();
            _map.OnGenerated.AddListener(MapGenerated);
        }

        void MapGenerated()
        {
            LocationNode closestNode = null;
            float minDistance = float.MaxValue;
            foreach (SceneNode node in _map.Nodes)
            {
                LocationNode location = node as LocationNode;
                if(!location) { continue; }

                location.Type = SelectOption();

                float distance = location.transform.position.magnitude;
                if(distance > minDistance) { continue; }
                closestNode = location;
                minDistance = distance;
            }
            
            if(!closestNode) { return; }
            
            closestNode.Type = LocationType.Building;
        }

        LocationType SelectOption()
        {
            if(_options.IsNullOrEmpty()) { return LocationType.Open; }

            float selectedWeight = Random.Range(0.0f, _totalWeight);
            int selectedIndex;
            for (selectedIndex = 0; selectedIndex < _options.Length; selectedIndex++)
            {
                if(selectedWeight < _options[selectedIndex].TotalWeight) { break; }
            }
            return _options[Mathf.Min(selectedIndex, _options.Length - 1)].Type;
        }

        void OnValidate()
        {
            _totalWeight = 0.0f;
            _options ??= Array.Empty<WeightedOption>();
            foreach (WeightedOption option in _options)
            {
                _totalWeight += option.Weight;
                option.TotalWeight = _totalWeight;
            }
        }
    }
}
