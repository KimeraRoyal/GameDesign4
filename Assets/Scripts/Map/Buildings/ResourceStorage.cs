using System;
using UnityEngine;

namespace Map.Buildings
{
    [Serializable]
    public class ResourceStorage
    {
        [SerializeField] ResourceType _type;
        
        [SerializeField] int _maxAmount;
        [SerializeField] int _currentAmount;

        public ResourceType Type => _type;

        public int MaxAmount => _maxAmount;
        public int CurrentAmount => _currentAmount;

        public bool IsEmpty => _currentAmount <= 0;
        public bool IsFull => _currentAmount >= _maxAmount;
        public int RemainingUntilFull => _maxAmount - _currentAmount;

        public int Add(int amount)
        {
            int previousAmount = _currentAmount;
            _currentAmount = Mathf.Min(_currentAmount + amount, _maxAmount);
            return _currentAmount - previousAmount;
        }

        public int Remove(int amount)
        {
            int previousAmount = _currentAmount;
            _currentAmount = Mathf.Max(_currentAmount - amount, 0);
            return previousAmount - _currentAmount;
        }

        public void Move(ResourceStorage other, int amount)
        {
            if(_type != other.Type) { return; }
            
            amount = Mathf.Min(amount, _currentAmount, other.RemainingUntilFull);
            Remove(amount);
            other.Add(amount);
        }
    }
}