using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Stock
{
    public class Ticker : MonoBehaviour
    {
        LineRenderer _line;
        
        [SerializeField] [Min(1)] int _pointCount = 10;
        [SerializeField] Vector2 _graphSize = Vector2.one;

        [SerializeField] bool _follow;
        [SerializeField] bool _shrinkToFit;
        [SerializeField] float _shrinkScale = 1.0f;

        float[] _points;
        float _offset;
        float _scale;
        
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

        public bool Follow
        {
            get => _follow;
            set => _follow = value;
        }

        public bool ShrinkToFit
        {
            get => _shrinkToFit;
            set => _shrinkToFit = value;
        }

        public UnityEvent<float, float> OnTick;

        void Awake()
        {
            _line = GetComponent<LineRenderer>();
            
            _points = new float[_pointCount];
            _positions = new Vector3[_pointCount + 2];
            _line.positionCount = _pointCount + 2;

            _offset = 0.0f;
            _scale = 1.0f;
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
            
            DoFollow();
            DoShrinkToFit();

            UpdateVisuals();
            OnTick?.Invoke(_points[^1], _points[^1] - _points[^2]);
        }

        private void UpdateVisuals()
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.y = _offset * (_graphSize.y * _scale);
            transform.localPosition = localPosition;
            
            for (int i = 0; i < _pointCount; i++)
            {
                _positions[i + 1].y = _points[i] * (_graphSize.y * _scale);
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

        void DoFollow()
            => _offset = _follow ? -_points[^1] : 0.0f;

        void DoShrinkToFit()
        {
            if(!_shrinkToFit) { return; }

            float maxDifference = 0.0f;
            float shrinkScale = _shrinkScale * _graphSize.y;
            foreach (float point in _points)
            {
                maxDifference = Mathf.Max(maxDifference, Mathf.Abs(point + _offset) - shrinkScale);
            }

            if (maxDifference < 0.001f)
            {
                _scale = 1.0f;
                return;
            }
            _scale = 1.0f / (1 + maxDifference);
        }
    }
}
