using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class EditorGameManager : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
