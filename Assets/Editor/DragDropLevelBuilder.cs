using System.IO;
using UnityEditor;
using UnityEngine;

public class DragDropLevelBuilder : EditorWindow
{
    [MenuItem("Window/Helsinki Tools/Drag-Drop Scene Setup")]
    public static void ShowWindow()
    {
        GetWindow<DragDropLevelBuilder>("Drag-Drop Setup");
    }

    private void OnGUI()
    {
        GUILayout.Label("Interactive Drag & Drop Sandbox", EditorStyles.boldLabel);
        GUILayout.Space(10);
        if (GUILayout.Button("Generate Sandbox", GUILayout.Height(30)))
        {
            CreateSandbox();
        }
    }

    private static void CreateSandbox()
    {
        // 1. Camera Setup
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            GameObject camObj = new GameObject("Main Camera");
            camObj.tag = "MainCamera";
            mainCam = camObj.AddComponent<Camera>();
            mainCam.orthographic = true;
            mainCam.orthographicSize = 5f;
            mainCam.backgroundColor = new Color(0.1f, 0.1f, 0.15f);
            mainCam.clearFlags = CameraClearFlags.SolidColor;
            Undo.RegisterCreatedObjectUndo(camObj, "Create Camera");
        }

        // 2. The Draggable Target
        GameObject targetObj = new GameObject("DraggableTarget");
        targetObj.transform.position = Vector3.zero;

        SpriteRenderer sr = targetObj.AddComponent<SpriteRenderer>();
        sr.sprite = GetOrGenerateCrispSprite();
        sr.color = new Color(0.9f, 0.2f, 0.2f); // Red Box

        targetObj.transform.localScale = new Vector3(1.5f, 1.5f, 1f);

        // Required for OnMouseDown to detect the object
        BoxCollider2D col = targetObj.AddComponent<BoxCollider2D>();
        col.size = new Vector2(1f, 1f);

        // 3. Inject the Script
        System.Type scriptType = System.Type.GetType("DraggableTimer, Assembly-CSharp");
        if (scriptType != null)
        {
            targetObj.AddComponent(scriptType);
        }
        else
        {
            Debug.LogWarning("[Helsinki Tools] Please create DraggableTimer.cs in your Scripts folder first.");
        }

        Undo.RegisterCreatedObjectUndo(targetObj, "Create Target");
        Selection.activeGameObject = targetObj;
        Debug.Log("[Helsinki Tools] Drag & Drop Sandbox generated.");
    }

    private static Sprite GetOrGenerateCrispSprite()
    {
        string assetPath = "Assets/Helsinki_SolidBlock.png";
        Sprite existingSprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
        if (existingSprite != null) return existingSprite;

        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        File.WriteAllBytes(Path.Combine(Application.dataPath, "Helsinki_SolidBlock.png"), tex.EncodeToPNG());
        DestroyImmediate(tex);
        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        return AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
    }
}