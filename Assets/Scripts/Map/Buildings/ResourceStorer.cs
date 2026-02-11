using UnityEngine;

namespace Map.Buildings
{
    public class ResourceStorer : Building, ResourceProvider
    {
        ResourceNetwork _network;

        [SerializeField] string _name;
        [SerializeField] [TextArea(3, 5)] string _information;

        [SerializeField] ResourceStorage _storage;
        [SerializeField] int _requestAmount = 100;
        [SerializeField] int _promised;
        
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
            => string.Format(_information, (float) _storage.CurrentAmount / _storage.MaxAmount * 100.0f);

        public Vector2Int GetPosition()
            => Location.Position;

        public ResourceType GetResourceType()
            => _storage.Type;

        public ResourceProviderCategory GetProviderCategory()
            => ResourceProviderCategory.Storer;

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
            => request.RequestedBy.GetProviderCategory() != ResourceProviderCategory.Storer && 
               request.Type == _storage.Type && _storage.CurrentAmount - _promised >= request.Amount;

        public void Promise(int amount)
            => _promised += amount;

        public void Give(ResourceType type, int amount)
        {
            _storage.Add(amount);
            _requestIsActive = false;
        }

        public void Take(ResourceType type, int amount)
        {
            _storage.Remove(amount);
            _promised -= amount;
        }
    }
}
