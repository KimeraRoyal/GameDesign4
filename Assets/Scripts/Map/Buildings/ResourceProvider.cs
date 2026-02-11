using UnityEngine;

namespace Map.Buildings
{
    public interface ResourceProvider
    {
        Vector2Int GetPosition();
        
        ResourceType GetResourceType();
        ResourceProviderCategory GetProviderCategory();
        
        int GetMaxAmount();
        int GetAvailableAmount();

        bool IsFull();
        int GetRemainingUntilFull();

        ResourceRequest PollForRequests();

        bool CanPromise(ResourceRequest request);
        void Promise(int amount);

        void Give(ResourceType type, int amount);
        void Take(ResourceType type, int amount);
    }

    public enum ResourceProviderCategory
    {
        Producer,
        Storer,
        Consumer
    }
}