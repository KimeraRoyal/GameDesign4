using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map.Buildings
{
    public class ResourceNetwork : MonoBehaviour
    {
        HashSet<ResourceProvider> _providers;

        Queue<ResourceRequest> _requests;
        Queue<ResourceRequest> _bouncedRequests;
        List<ResourceRequest> _requestsInProgress;

        [SerializeField] float _pollInterval = 1.0f;
        float _pollTimer;
        
        void Awake()
        {
            _providers = new HashSet<ResourceProvider>();

            _requests = new Queue<ResourceRequest>();
            _bouncedRequests = new Queue<ResourceRequest>();
            _requestsInProgress = new List<ResourceRequest>();
        }

        void Update()
        {
            _pollTimer += Time.deltaTime;
            if(_pollTimer < _pollInterval) { return; }
            _pollTimer -= _pollInterval;

            PollForRequests();
            PollRequests();
            ResolveRequests();
        }

        public void Register(ResourceProvider provider)
            => _providers.Add(provider);

        void PollForRequests()
        {
            foreach (ResourceRequest request in _providers.Select(provider => provider.PollForRequests()).Where(request => request != null))
            {
                _requests.Enqueue(request);
            }
        }

        void PollRequests()
        {
            while (_requests.Count > 0)
            {
                ResourceRequest request = _requests.Dequeue();

                if (_providers.OrderBy(provider => Vector2Int.Distance(request.RequestedBy.GetPosition(), provider.GetPosition()))
                    .Any(provider => request.Promise(provider)))
                {
                    Debug.Log($"Sent Request from {request.RequestedBy} to {request.PromisedBy}");
                    _requestsInProgress.Add(request);
                }
                else
                {
                    _bouncedRequests.Enqueue(request);
                }
            }
            (_requests, _bouncedRequests) = (_bouncedRequests, _requests);
        }

        void ResolveRequests()
        {
            foreach (ResourceRequest request in _requestsInProgress)
            {
                request.Fulfill();
            }
            _requestsInProgress.Clear();
        }
    }
}