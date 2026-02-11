using UnityEngine;

namespace Map.Buildings
{
    public class ResourceRequest
    {
        ResourceProvider _requestedBy;
        
        ResourceType _type;
        int _amount;

        ResourceProvider _promisedBy;
        bool _fulfilled;

        public ResourceProvider RequestedBy => _requestedBy;

        public ResourceType Type => _type;
        public int Amount => _amount;

        public ResourceProvider PromisedBy => _promisedBy;
        public bool Fulfilled => _fulfilled;

        public ResourceRequest(ResourceProvider requestedBy, ResourceType type, int amount)
        {
            _requestedBy = requestedBy;

            _type = type;
            _amount = amount;
        }

        public bool Promise(ResourceProvider provider)
        {
            if(_requestedBy == provider || !provider.CanPromise(this)) { return false; }

            provider.Promise(_amount);
            _promisedBy = provider;
            
            return true;
        }

        public void Fulfill()
        {
            _promisedBy.Take(_type, _amount);
            _requestedBy.Give(_type, _amount);
        }
    }
}