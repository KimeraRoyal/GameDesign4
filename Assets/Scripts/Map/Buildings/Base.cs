using UnityEngine;

namespace Map.Buildings
{
    public class Base : Building, ResourceProvider
    {
        ResourceNetwork _network;
        
        [SerializeField] string _name;
        [SerializeField] string _information;

        [SerializeField] ResourceStorage _storage;
        [SerializeField] int _requestAmount = 100;
        
        bool _requestIsActive;

        protected override void Awake()
        {
            base.Awake();

            _network = GetComponentInParent<ResourceNetwork>();
        }

        void Start()
        {
            _network.Register(this);
        }

        public override string GetName()
            => _name;

        public override string GetInformation()
            => _information;

        public Vector2Int GetPosition()
            => Location.Position;

        public ResourceType GetResourceType()
            => _storage.Type;

        public ResourceProviderCategory GetProviderCategory()
            => ResourceProviderCategory.Consumer;

        public int GetMaxAmount()
            => _storage.MaxAmount;

        public int GetAvailableAmount()
            => _storage.CurrentAmount;

        public bool IsFull()
            => _storage.IsFull;

        public int GetRemainingUntilFull()
            => _storage.RemainingUntilFull;

        public ResourceRequest PollForRequests()
        {
            if (_requestIsActive || _storage.RemainingUntilFull < _requestAmount) { return null; }

            _requestIsActive = true;
            return new ResourceRequest(this, _storage.Type, _requestAmount);
        }

        public bool CanPromise(ResourceRequest request)
            => false;

        public void Promise(int amount) { }

        public void Give(ResourceType type, int amount)
        {
            _storage.Add(amount);
            _requestIsActive = false;
        }

        public void Take(ResourceType type, int amount) { }
    }
}
