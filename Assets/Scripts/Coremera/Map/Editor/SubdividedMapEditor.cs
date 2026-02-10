using UnityEditor;
using UnityEngine;

namespace Map.Editor
{
    [CustomEditor(typeof(SubdividedMap))]
    public class SubdividedMapEditor : MapEditor
    {
        SerializedProperty _border;
        
        SerializedProperty _showDivisions;
        SerializedProperty _divisions;
        
        void Awake()
        {
            _border = serializedObject.FindProperty("_border");
            
            _showDivisions = serializedObject.FindProperty("_showDivisions");
            _divisions = serializedObject.FindProperty("_divisions");
        }

        protected override void OnSceneGUI()
        {
            DrawSceneGUI();
            base.OnSceneGUI();
        }

        private void DrawSceneGUI()
        {
            SubdividedMap map = target as SubdividedMap;
            if (!map) { return; }

            DrawDivisions();
            
            Vector2 border = _border.vector2Value;
            
            Handles.color = Color.red;
            Handles.DrawWireCube(map.transform.position, map.Size - border * 2.0f);
        }

        private void DrawDivisions()
        {
            if(!_showDivisions.boolValue && _divisions.arraySize < 1) { return; }

            Handles.color = Color.green;
            foreach (SerializedProperty division in _divisions)
            {
                Rect rect = division.rectValue;
                Handles.DrawWireCube(rect.center, rect.size);
            }
            
        }
    }
}