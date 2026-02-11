using Map.Scene;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    public class LocationNode : SceneNode
    {
        [SerializeField] Vector2Int _position;
        
        [SerializeField] SpriteRenderer _renderer;
        
        [SerializeField] LocationType _type;
        [SerializeField] Building _building;

        public Vector2Int Position => _position;

        public LocationType Type
        {
            get => _type;
            set
            {
                _type = value;
                _renderer.sprite = _type.TileSprite;
            }
        }

        public Building Building => _building;

        public UnityEvent OnForceStateChange;

        protected override void Awake()
        {
            base.Awake();

            _position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }

        public Building SpawnBuilding(Building buildingPrefab)
        {
            if(_building) { return null; }
            
            _building = Instantiate(buildingPrefab, transform);
            
            OnForceStateChange?.Invoke();
            return _building;
        }

        public void DemolishBuilding()
        {
            if(!_building || !_building.CanBeDemolished) { return; }
            
            Destroy(_building.gameObject);
            _building = null;
            
            OnForceStateChange?.Invoke();
        }

        public string GetName()
            => _building ? _building.GetName() : _type.name;

        public string GetInformation()
            => _building ? _building.GetInformation() : _type.GetTileInformation(this);
    }
}
