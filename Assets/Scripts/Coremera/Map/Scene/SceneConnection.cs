using UnityEngine;

namespace Map.Scene
{
    public class SceneConnection : MonoBehaviour
    {
        Connection _connection;
        
        SceneMap _map;

        LineRenderer _line;

        public SceneNode A { get; private set; }
        public SceneNode B { get; private set; }

        void Awake()
        {
            _map = GetComponentInParent<SceneMap>();

            _line = GetComponent<LineRenderer>();
        }

        public void Assign(Connection connection)
        {
            if(_connection != null) { return; }

            _connection = connection;

            A = _map.GetNode(connection.A);
            B = _map.GetNode(connection.B);

            Vector2 distance = (connection.B.Position - connection.A.Position) / 2.0f;
            transform.position = connection.A.Position + distance;

            _line.positionCount = 2;
            var positions = new[] { -(Vector3) distance, (Vector3) distance };
            _line.SetPositions(positions);
        }
    }
}