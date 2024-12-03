using UnityEditor;
using UnityEngine;

public class PrefabSpawnerEditor : EditorWindow
{
    [MenuItem("CustomPrefabSpawner/Prefab Spawner")]
    public static void ShowWindow()
    {
        // Create a new window
        EditorWindow.GetWindow(typeof(PrefabSpawnerEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefab Spawner", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Prefab Spawner"))
        {
            // Create a new empty GameObject and add the PrefabSpawner script to it
            GameObject go = new GameObject("Prefab Spawner");
            go.AddComponent<PrefabSpawner>();

            // Print confirmation in the console
            Debug.Log("Create custom prefab spawnable.");
        }
    }
}
