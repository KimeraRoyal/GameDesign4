using System;
using UnityEditor;
using UnityEngine;

namespace Map.Editor
{
    [CustomEditor(typeof(PredefinedMap))]
    public class PredefinedMapEditor : MapEditor
    {
        SerializedProperty _nodePositions;
        
        void Awake()
        {
            _nodePositions = serializedObject.FindProperty("_nodePositions");
        }

        protected override void OnSceneGUI()
        {
            PredefinedMap map = target as PredefinedMap;
            if (!map) { return; }

            if (map.Nodes != null)
            {
                base.OnSceneGUI();
                return;
            }

            SerializedProperty currentPosition = _nodePositions.GetArrayElementAtIndex(0);

            Vector2 previousPosition = Vector2.zero;
            bool hasPreviousPosition = false;
            
            EditorGUI.BeginChangeCheck();
            for(int i = 0; i < _nodePositions.arraySize; i++, currentPosition.Next(false))
            {
                currentPosition.vector2Value = Handles.FreeMoveHandle(currentPosition.vector2Value, 0.1f, Vector3.one * 0.5f, Handles.RectangleHandleCap);

                if (hasPreviousPosition) { Handles.DrawLine(previousPosition, currentPosition.vector2Value); }
                previousPosition = currentPosition.vector2Value;
                hasPreviousPosition = true;
            }
            EditorGUI.EndChangeCheck();

            serializedObject.ApplyModifiedProperties();
        }
    }
}