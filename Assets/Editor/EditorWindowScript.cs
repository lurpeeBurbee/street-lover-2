using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EditorWindowScript : EditorWindow
{
    private string scenesFolder = "Assets/Scenes";
    private string assetsFolder = "Assets/Assets";
    private string assetName = "NewAsset";

    [MenuItem("Tools/Asset Creator")]
    public static void ShowWindow()
    {
        GetWindow<EditorWindowScript>("Asset Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Default Folders", EditorStyles.boldLabel);

        scenesFolder = EditorGUILayout.TextField("Scenes Folder", scenesFolder);
        assetsFolder = EditorGUILayout.TextField("Assets Folder", assetsFolder);

        GUILayout.Label("Create Asset", EditorStyles.boldLabel);
        assetName = EditorGUILayout.TextField("Asset Name", assetName);

        if (GUILayout.Button("Create Scene"))
        {
            CreateScene();
        }

        if (GUILayout.Button("Create Asset"))
        {
            CreateAsset();
        }
    }

    private void CreateScene()
    {
        string path = $"{scenesFolder}/{assetName}.unity";
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);
        AssetDatabase.Refresh();
        Debug.Log($"Scene created at {path}");
    }

    private void CreateAsset()
    {
        string path = $"{assetsFolder}/{assetName}.asset";
        ScriptableObject asset = ScriptableObject.CreateInstance<ScriptableObject>();
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Asset created at {path}");
    }
}
