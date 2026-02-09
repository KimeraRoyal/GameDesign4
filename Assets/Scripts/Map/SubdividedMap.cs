using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    public class SubdividedMap : Map
    {
        private static readonly System.Random Rng = new();

        [SerializeField] uint _iterations = 4;

        [SerializeField] [MinMaxSlider(0.0f, 1.0f)] private Vector2 _splitRange = new(0.45f, 0.55f);

        [SerializeField] Vector2 _minSubdivisionSize;

        [SerializeField] Vector2 _border;
        [SerializeField] Vector2 _subdivisionPadding;
        
        [SerializeField] int _maxNodes = 10;
        [SerializeField] [MinMaxSlider(0.0f, 1.0f)] private Vector2 _nodePositionRange = new(0.45f, 0.55f);
        [SerializeField] Vector2 _nodeClamp;

        RectDivider _rectDivider;

        protected override void Populate()
        {
            Rect bounds = new((Vector2) transform.position - Size / 2.0f, Size);
            _rectDivider = RectDivider.Divide(bounds, _iterations, _splitRange, _minSubdivisionSize, _border, _subdivisionPadding);
            
            int[] divisionIndices = Enumerable.Range(0, _rectDivider.Divisions.Count).ToArray();
            if (_maxNodes >= 0)
            {
                int nodeCount = Mathf.Min(_rectDivider.Divisions.Count, _maxNodes);
                divisionIndices = divisionIndices.OrderBy(_ => Rng.Next()).ToArray()[..nodeCount];
            }
            
            Nodes.Clear();
            foreach (int i in divisionIndices)
            {
                Vector2 minNodePosition = Vector2.Lerp(_rectDivider.Divisions[i].min, _rectDivider.Divisions[i].max, _nodePositionRange.x);
                Vector2 maxNodePosition = Vector2.Lerp(_rectDivider.Divisions[i].min, _rectDivider.Divisions[i].max, _nodePositionRange.y);
                
                Vector2 nodePosition = new(Random.Range(minNodePosition.x, maxNodePosition.x), Random.Range(minNodePosition.y, maxNodePosition.y));
                if (_nodeClamp.x > 0.001f) { nodePosition.x = Mathf.Round(nodePosition.x / _nodeClamp.x) * _nodeClamp.x; }
                if (_nodeClamp.y > 0.001f) { nodePosition.y = Mathf.Round(nodePosition.y / _nodeClamp.y) * _nodeClamp.y; }
                
                Nodes.Add(new Node(nodePosition));
            }
        }
    }
}
