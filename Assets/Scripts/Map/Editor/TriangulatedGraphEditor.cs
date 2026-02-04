using Map.Map;
using UnityEditor;
using UnityEngine;

namespace Map.Editor
{
    [CustomEditor(typeof(MapGenerator))]
    public class TriangulatedGraphEditor : UnityEditor.Editor
    {
        SerializedProperty points;
        
        void OnEnable()
        {
            points = serializedObject.FindProperty("_points");
        }

        void OnSceneGUI()
        {
            Handles.color = Color.yellow;

            SerializedProperty currentPoint = points.GetArrayElementAtIndex(0);
            for (int i = 0; i < points.arraySize; i++, currentPoint.Next(false))
            {
                Handles.DrawSolidDisc(currentPoint.vector2Value, Vector3.forward, 0.1f);
            }
        }
    }
}
