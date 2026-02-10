using Input;
using Map.Scene;
using UnityEngine;

namespace Map
{
    public class LocationNode : SceneNode
    {
        [SerializeField] SpriteRenderer _renderer;
        
        [SerializeField] LocationType _type;
        [SerializeField] Building _building;

        [SerializeField] Sprite[] _sprites;

        public LocationType Type
        {
            get => _type;
            set
            {
                _type = value;
                _renderer.sprite = _sprites[(int)_type];
            }
        }
    }
}
