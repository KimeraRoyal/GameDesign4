using UnityEditor;
using UnityEngine;

namespace Map.Editor
{
    [CustomEditor(typeof(Map))]
    public class MapEditor : UnityEditor.Editor
    {
        protected virtual void OnSceneGUI()
        {
            Map map = target as Map;
            if (!map) { return; }

            Handles.color = Color.green;
            Handles.DrawWireCube(map.transform.position, map.Size);
            
            if(map.Nodes == null) { return; }
            
            EditorGUI.BeginChangeCheck();
            foreach (Node node in map.Nodes)
            {
                node.Position = Handles.FreeMoveHandle(node.Position, 0.1f, Vector3.one * 0.5f, Handles.RectangleHandleCap);
            }
            EditorGUI.EndChangeCheck();
        }
    }
}
