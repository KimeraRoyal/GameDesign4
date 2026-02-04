using UnityEditor;
using UnityEngine;

namespace NodeGraph.Editor
{
    [CustomEditor(typeof(TriangulatedGraph))]
    public class TriangulatedGraphEditor : UnityEditor.Editor
    {
        SerializedProperty points;
        
        void OnEnable()
        {
            points = serializedObject.FindProperty("_points");
        }

        void OnSceneGUI()
        {
            TriangulatedGraph graph = (TriangulatedGraph)target;
            
            Handles.color = Color.yellow;

            SerializedProperty currentPoint = points.GetArrayElementAtIndex(0);
            for (int i = 0; i < points.arraySize; i++, currentPoint.Next(false))
            {
                Handles.DrawSolidDisc(currentPoint.vector2Value, Vector3.forward, 0.1f);
            }
        }
    }
}
