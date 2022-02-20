using UnityEngine;
using UnityEditor;

namespace GridSystem
{


    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor : Editor
    {
        private GridManager Target
        {
            get => target as GridManager;

        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Grid"))
            {
                if (Target.Grid != null)
                {
                    Target.transform.ClearAllChildren();
                }
                Target.GenerateGrid();
            }

            if (GUILayout.Button("Clear Grid"))
            {
                Target.transform.ClearAllChildren();
                Target.ClearGrid();
            }
            if (GUILayout.Button("Toggle Debugmode"))
            {
                Target.Grid.ToogleDebug(!Target.Grid.DebugMode);
            }
        }
    }
}