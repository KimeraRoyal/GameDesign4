using UnityEngine;
using Random = UnityEngine.Random;

namespace Stock
{
    [RequireComponent(typeof(Ticker))]
    public class AutoTicker : MonoBehaviour
    {
        Ticker _ticker;

        [SerializeField] Vector2 _incrementRange = new(-1.0f, 1.0f);
        
        [SerializeField] float _tickInterval = 1.0f;
        float _tickTimer;

        float _currentValue;

        void Awake()
        {
            _ticker = GetComponent<Ticker>();
        }

        void Update()
        {
            _tickTimer += Time.deltaTime;
            if(_tickTimer < _tickInterval) { return; }
            _tickTimer -= _tickInterval;

            _currentValue += Random.Range(_incrementRange.x, _incrementRange.y);
            _ticker.Tick(_currentValue);
        }
    }
}
