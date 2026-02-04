using UnityEngine;

namespace Map.Map
{
    public class Node
    {
        public Vector2 Position { get; set; }
        public int ID { get; set; }

        public Node(Vector2 position, int id)
        {
            Position = position;
            ID = id;
        }
    }
}