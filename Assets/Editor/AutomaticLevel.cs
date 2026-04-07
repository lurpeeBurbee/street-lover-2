using System.IO;
using UnityEditor;
using UnityEngine;

public class AutomaticLevel : EditorWindow
{
    [MenuItem("Window/Helsinki Tools/Automatic Level")]
    public static void ShowWindow()
    {
        GetWindow<AutomaticLevel>("Automatic Level");
    }

    private const float OrthoSize = 5f;
    private const float AspectRatio = 16f / 9f;
    private const float GroundHeight = 2f;
    private const float GroundBottomY = -5f;

    private void OnGUI()
    {
        GUILayout.Label("Helsinki Automatic Level Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(
            "NOTE:\n" +
            "Requires Game View set to Full HD (1920x1080) and Main Camera Orthographic Size = 5.\n\n" +
            "This tool generates a 1x1 solid block, the Player, a single Ground element, and a World-Space Background Sprite.",
            MessageType.Info);

        GUILayout.Space(15);

        if (GUILayout.Button("Generate Automatic Level", GUILayout.Height(30)))
        {
            CreateLevel();
        }
    }

    private static void CreateLevel()
    {
        // 1. Environment Layers
        CreateLayerIfNeeded("Ground");
        CreateLayerIfNeeded("Player");

        // 2. Camera Math
        float camHeight = OrthoSize * 2f;
        float camWidth = camHeight * AspectRatio;
        float groundCentreY = GroundBottomY + (GroundHeight / 2f);

        GameObject envRoot = new GameObject("Environment");

        // 3. Single Ground Element (Spans the full camera width)
        GameObject groundObj = CreateBaseGroundElement("Main Ground", new Vector2(0f, groundCentreY), new Vector2(camWidth, GroundHeight), new Color(0.2f, 0.8f, 0.2f), "Ground");
        groundObj.transform.SetParent(envRoot.transform);

        // 4. Automate Player
        GameObject playerObj = CreatePlayer();

        // 5. Automate Camera & Background Sprite
        SetupCameraAndBackground(camWidth, camHeight);

        Undo.RegisterCreatedObjectUndo(envRoot, "Create Automatic Level");
        Selection.activeGameObject = playerObj;

        Debug.Log($"[Helsinki Tools] Automatic Level with World-Space Background generated perfectly.");
    }

    private static GameObject CreatePlayer()
    {
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.layer = LayerMask.NameToLayer("Player");
        player.transform.position = Vector3.zero;

        SpriteRenderer sr = player.AddComponent<SpriteRenderer>();
        sr.sprite = GetOrGenerateCrispSprite();
        sr.color = Color.cyan;
        player.transform.localScale = new Vector3(1f, 2f, 1f);

        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        CapsuleCollider2D col = player.AddComponent<CapsuleCollider2D>();
        col.size = new Vector2(1f, 1f);

        Undo.RegisterCreatedObjectUndo(player, "Create Player");
        return player;
    }

    private static void SetupCameraAndBackground(float screenWidth, float screenHeight)
    {
        // 1. MAIN CAMERA SETUP
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            GameObject camObj = new GameObject("Main Camera");
            camObj.tag = "MainCamera";
            mainCam = camObj.AddComponent<Camera>();
            mainCam.orthographic = true;
            mainCam.orthographicSize = OrthoSize;
            Undo.RegisterCreatedObjectUndo(camObj, "Create Main Camera");
        }

        // 2. WORLD-SPACE BACKGROUND SETUP
        GameObject bgObj = new GameObject("BackgroundSprite");

        SpriteRenderer sr = bgObj.AddComponent<SpriteRenderer>();
        sr.sprite = GetOrGenerateCrispSprite();
        sr.color = new Color(0.1f, 0.1f, 0.1f, 1f); // Dark grey/black backdrop

        // Push it far behind the gameplay layers
        sr.sortingOrder = -10;

        // Mathematically stretch the 1x1 sprite to fill the exact camera lens
        bgObj.transform.localScale = new Vector3(screenWidth, screenHeight, 1f);

        // Snap it directly to the camera's center
        bgObj.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, 0f);

        Undo.RegisterCreatedObjectUndo(bgObj, "Create Background");
    }

    private static GameObject CreateBaseGroundElement(string name, Vector2 position, Vector2 worldSize, Color color, string layerName)
    {
        GameObject obj = new GameObject(name);
        obj.transform.position = new Vector3(position.x, position.y, 0f);
        obj.layer = LayerMask.NameToLayer(layerName);

        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = GetOrGenerateCrispSprite();
        sr.color = color;

        obj.transform.localScale = new Vector3(worldSize.x, worldSize.y, 1f);

        BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
        col.size = new Vector2(1f, 1f);

        return obj;
    }

    private static Sprite GetOrGenerateCrispSprite()
    {
        string assetPath = "Assets/Helsinki_SolidBlock.png";
        Sprite existingSprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
        if (existingSprite != null) return existingSprite;

        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();

        string absolutePath = Path.Combine(Application.dataPath, "Helsinki_SolidBlock.png").Replace("\\", "/");
        File.WriteAllBytes(absolutePath, tex.EncodeToPNG());
        DestroyImmediate(tex);
        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.spritePixelsPerUnit = 1;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SaveAndReimport();
        }

        return AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
    }

    private static void CreateLayerIfNeeded(string layerName)
    {
        SerializedObject tagManager = new(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layers = tagManager.FindProperty("layers");

        for (int i = 8; i < layers.arraySize; i++)
        {
            if (layers.GetArrayElementAtIndex(i).stringValue == layerName) return;
        }

        for (int i = 8; i < layers.arraySize; i++)
        {
            SerializedProperty slot = layers.GetArrayElementAtIndex(i);
            if (string.IsNullOrEmpty(slot.stringValue))
            {
                slot.stringValue = layerName;
                tagManager.ApplyModifiedProperties();
                return;
            }
        }
    }
}