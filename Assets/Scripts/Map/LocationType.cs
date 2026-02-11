using Sirenix.Utilities;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(fileName = "New Location Type", menuName = "GD4/Location Type")]
    public class LocationType : ScriptableObject
    {
        [SerializeField] Sprite _tileSprite;

        [SerializeField] Building[] _supportedBuildings;

        [SerializeField] string _tileInformation;
        [SerializeField] bool _informationUpdates;

        public Sprite TileSprite => _tileSprite;

        public Building[] SupportedBuildings => _supportedBuildings;
        public bool SupportsBuildings => !_supportedBuildings.IsNullOrEmpty();

        public bool InformationUpdates => _informationUpdates;

        public virtual string GetTileInformation(LocationNode location)
            => _tileInformation;
    }
}