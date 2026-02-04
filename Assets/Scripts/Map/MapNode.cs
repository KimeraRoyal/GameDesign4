using UnityEngine;

namespace Map.Map
{
    public class MapNode
    {
        public Vector2 Position { get; set; }

        public MapNode(Vector2 position)
        {
            Position = position;
        }
    }
}