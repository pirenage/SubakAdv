using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemContainer))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemContainer container = target as ItemContainer;
        if (GUILayout.Button("Clear Containers"))
        {
            for (int i = 0; i < container.slots.Count; i++)
            {
                container.slots[i].Clear();
            }
        }
        DrawDefaultInspector();
    }
}