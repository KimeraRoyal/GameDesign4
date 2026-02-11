using UnityEngine;

namespace Map.Buildings
{
    public class ResourceProducer : Building, ResourceProvider
    {
        ResourceNetwork _network;
        
        [SerializeField] string _name;
        [SerializeField] [TextArea(3, 5)] string _information;

        [SerializeField] float _productionRate = 1.0f;
        [SerializeField] float _productionBottleneck = 5.0f;
        float _production;
        
        [SerializeField] ResourceStorage _storage;
        [SerializeField] int _promised;

        protected override void Awake()
        {
            base.Awake();

            _network = GetComponentInParent<ResourceNetwork>();
        }

        void Start()
        {
            _network.Register(this);
        }

        void Update()
        {
            _production = Mathf.Min(_production + _productionRate * Time.deltaTime, _productionBottleneck);
            if(_storage.IsFull || _production < 1.0f) { return; }

            int producedAmount = Mathf.FloorToInt(_production);
            _production -= _storage.Add(producedAmount);
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
            => ResourceProviderCategory.Producer;

        public int GetMaxAmount()
            => _storage.MaxAmount;

        public int GetAvailableAmount()
            => _storage.CurrentAmount;

        public bool IsFull()
            => _storage.IsFull;

        public int GetRemainingUntilFull()
            => _storage.RemainingUntilFull;

        public ResourceRequest PollForRequests()
            => null;

        public bool CanPromise(ResourceRequest request)
            => request.Type == _storage.Type && _storage.CurrentAmount - _promised >= request.Amount;

        public void Promise(int amount)
            => _promised += amount;

        public void Give(ResourceType type, int amount)
            => _storage.Add(amount);

        public void Take(ResourceType type, int amount)
        {
            _storage.Remove(amount);
            _promised -= amount;
        }
    }
}
