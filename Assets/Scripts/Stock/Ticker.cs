using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Stock
{
    public class Ticker : MonoBehaviour
    {
        LineRenderer _line;
        
        [SerializeField] [Min(1)] int _pointCount = 10;
        [SerializeField] Vector2 _graphSize = Vector2.one;

        [SerializeField] float[] _points;
        Vector3[] _positions;

        bool _dirty;

        public Vector2 GraphSize
        {
            get => _graphSize;
            set
            {
                _graphSize = value;
                _dirty = true;
            }
        }
        
        void Awake()
        {
            _line = GetComponent<LineRenderer>();
            
            _points = new float[_pointCount];
            _positions = new Vector3[_pointCount + 2];
            _line.positionCount = _pointCount + 2;
        }

        void Start()
        {
            RegenerateGraph(true);
            _line.SetPositions(_positions);
        }

        [Button("Tick")]
        void TestTick()
            => Tick(Random.Range(-1.0f, 1.0f));

        public void Tick(float newValue)
        {
            RegenerateGraph();
            
            Array.Copy(_points, 1, _points, 0, _points.Length - 1);
            _points[^1] = newValue;
            
            for (int i = 0; i < _pointCount; i++)
            {
                _positions[i + 1].y = _points[i] * _graphSize.y;
            }
            _positions[0].y = _positions[1].y;
            _positions[^1].y = _positions[^2].y;
            
            _line.SetPositions(_positions);
        }

        void RegenerateGraph(bool force = false)
        {
            if(!force && _dirty) { return; }
            
            float pointScale = _graphSize.x / (_pointCount - 1);
            float xOffset = (_pointCount - 1) / 2.0f;
            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i].x = (i - 1 - xOffset) * pointScale;
            }

            _dirty = false;
        }
    }
}
