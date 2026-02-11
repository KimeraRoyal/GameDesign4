using UnityEngine;

namespace Map
{
    public abstract class Building : MonoBehaviour
    {
        LocationNode _location;
        
        [SerializeField] Sprite _buttonSprite;

        [SerializeField] bool _canBeDemolished = true;

        public LocationNode Location => _location;

        public Sprite ButtonSprite => _buttonSprite;

        public bool CanBeDemolished => _canBeDemolished;

        protected virtual void Awake()
        {
            _location = GetComponentInParent<LocationNode>();
        }

        public abstract string GetName();
        public abstract string GetInformation();
    }
}