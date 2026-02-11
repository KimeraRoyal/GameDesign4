using System;
using UnityEngine;

namespace Map.Buildings
{
    public class ResourceTransformer : Building, ResourceProvider
    {
        ResourceNetwork _network;

        [SerializeField] string _name;
        [SerializeField] [TextArea(3, 5)] string _information;

        [SerializeField] float _processInterval = 1.0f;
        [SerializeField] int _maxBatches = 1;
        float _processTimer;

        [SerializeField] int _bottleneck = 5;
        int _buffer;

        [SerializeField] int _inRatio = 1;
        [SerializeField] ResourceStorage _in;
        [SerializeField] int _requestAmount = 100;

        [SerializeField] int _outRatio = 1;
        [SerializeField] ResourceStorage _out;
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

        void Update()
        {
            if (_in.IsEmpty)
            {
                _processTimer = 0.0f;
                return;
            }
            
            _processTimer += Time.deltaTime;
            if(_processTimer < _processInterval) { return; }
            _processTimer -= _processInterval;

            for (int i = 0; i < _maxBatches && _in.CurrentAmount >= _inRatio && _buffer < _bottleneck; i++)
            {
                _in.Remove(_inRatio);
                _buffer = Mathf.Min(_buffer + _outRatio, _bottleneck);
            }
            _buffer -= _out.Add(_buffer);
        }

        public override string GetName()
            => _name;

        public override string GetInformation()
            => string.Format(_information, (float) _in.CurrentAmount / _in.MaxAmount * 100.0f, (float) _out.CurrentAmount / _out.MaxAmount * 100.0f);

        public Vector2Int GetPosition()
            => Location.Position;

        public ResourceType GetResourceType()
            => _out.Type;

        public ResourceProviderCategory GetProviderCategory()
            => ResourceProviderCategory.Consumer;

        public int GetMaxAmount()
            => _out.MaxAmount;

        public int GetAvailableAmount()
            => _out.CurrentAmount;

        public bool IsFull()
            => _out.IsFull;

        public int GetRemainingUntilFull()
            => _out.RemainingUntilFull;

        public ResourceRequest PollForRequests()
        {
            if (_requestIsActive || _in.RemainingUntilFull < _requestAmount) { return null; }

            _requestIsActive = true;
            return new ResourceRequest(this, _in.Type, _requestAmount);
        }

        public bool CanPromise(ResourceRequest request)
            => request.Type == _out.Type && _out.CurrentAmount - _promised >= request.Amount;

        public void Promise(int amount)
            => _promised += amount;

        public void Give(ResourceType type, int amount)
        {
            _in.Add(amount);
            _requestIsActive = false;
        }

        public void Take(ResourceType type, int amount)
        {
            _out.Remove(amount);
            _promised -= amount;
        }
    }
}
