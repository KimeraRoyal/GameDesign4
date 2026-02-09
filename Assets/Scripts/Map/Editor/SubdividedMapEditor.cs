using UnityEditor;
using UnityEngine;

namespace Map.Editor
{
    [CustomEditor(typeof(SubdividedMap))]
    public class SubdividedMapEditor : MapEditor
    {
        SerializedProperty _border;
        
        void Awake()
        {
            _border = serializedObject.FindProperty("_border");
        }

        protected override void OnSceneGUI()
        {
            DrawSceneGUI();
            base.OnSceneGUI();
        }

        private void DrawSceneGUI()
        {
            Map map = target as SubdividedMap;
            if (!map) { return; }
            
            Vector2 border = _border.vector2Value;
            
            Handles.color = Color.red;
            Handles.DrawWireCube(map.transform.position, map.Size - border * 2.0f);
        }
    }
}