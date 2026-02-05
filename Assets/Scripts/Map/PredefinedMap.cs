using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public class PredefinedMap : Map
    {
        [SerializeField] List<Vector2> _nodePositions;
        
        protected override void Populate()
        {
            Nodes.AddRange(_nodePositions.Select(position => new Node(position)));
        }
    }
}