using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Grid grid = (Grid) target;
        
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            grid.Generate();
        }
    }
}
