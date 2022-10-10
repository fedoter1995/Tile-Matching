using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Table))]
public class TableEditor : Editor
{

    int keyNumber = 1;
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        Table table = (Table)target;
        if(table.FilledSlots.Count > 0)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Slot number");
            EditorGUILayout.LabelField("Tiles");
            EditorGUILayout.EndHorizontal();
        }

        for (int i= 0; i < table.FilledSlots.Count; i++)
        {
                EditorGUILayout.BeginHorizontal("Button");
                table.FilledSlots[i] = EditorGUILayout.IntField(table.FilledSlots[i]);
                table.Tiles[i] = (Tile)EditorGUILayout.ObjectField(table.Tiles[i], typeof(Tile), true);
                EditorGUILayout.EndHorizontal();
        }
        var button = EditorGUILayout.BeginHorizontal("Button");

        if (GUILayout.Button("Add filled slot") && table.GridZize > table.FilledSlots.Count)
        {
            table.FilledSlots.Add(keyNumber);
            table.Tiles.Add(null);
        }
        if (GUILayout.Button("Remove filled slot") && table.FilledSlots.Count != 0)
        {
            var intList = new List<int>(table.FilledSlots);
            var tilesList = new List<Tile>(table.Tiles);

            for(int i = 0; i < intList.Count; i++)
            {
                if(intList[i] == keyNumber)
                {
                    table.FilledSlots.Remove(table.FilledSlots[i]);
                    table.Tiles.Remove(table.Tiles[i]);
                }
            }
        }
        if (keyNumber < 1)
            keyNumber = 1;
        keyNumber = EditorGUILayout.IntField(keyNumber);

        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Reset Table"))
        {
            table.ResetGrid();
        }

        EditorUtility.SetDirty(table);
    }
}
