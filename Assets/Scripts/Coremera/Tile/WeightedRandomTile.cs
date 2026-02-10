using System;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Coremera.Tile
{
    [CreateAssetMenu(fileName = "New Weighted Random Tile", menuName = "2D/Tiles/Weighted Random Tile")]
    public class WeightedRandomTile : TileBase
    {
        [Serializable]
        public class WeightedOption
        {
            [SerializeField] Sprite _sprite;
            
            [SerializeField] float _weight = 1.0f;
            [SerializeField] float _totalWeight = 0.0f;
            
            public Sprite Sprite => _sprite;
            
            public float Weight => _weight;

            public float TotalWeight
            {
                get => _totalWeight;
                set => _totalWeight = value;
            }
        }
        
        [SerializeField] WeightedOption[] _options;
        [SerializeField] float _totalWeight;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = SelectOption();
            tileData.color = Color.white;
            
            tileData.transform = Matrix4x4.identity;
            tileData.colliderType = UnityEngine.Tilemaps.Tile.ColliderType.Grid;
        }

        Sprite SelectOption()
        {
            if(_options.IsNullOrEmpty()) { return null; }

            float selectedWeight = Random.Range(0.0f, _totalWeight);
            int selectedIndex;
            for (selectedIndex = 0; selectedIndex < _options.Length; selectedIndex++)
            {
                if(selectedWeight < _options[selectedIndex].TotalWeight) { break; }
            }
            return _options[Mathf.Min(selectedIndex, _options.Length - 1)].Sprite;
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
