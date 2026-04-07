using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class HelsinkiLevelBuilder : EditorWindow
{
    [MenuItem("Window/Helsinki Tools/Level Builder")]
    public static void ShowWindow()
    {
        GetWindow<HelsinkiLevelBuilder>("Level Builder");
    }

    private const float OrthoSize = 5f;
    private const float AspectRatio = 16f / 9f;
    private const float GroundHeight = 2f;
    private const float GroundBottomY = -5f;

    private void OnGUI()
    {
        GUILayout.Label("Helsinki Battlestation Setup", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox(
            "NOTE:\n" +
            "Requires Game View set to Full HD (1920x1080) and Main Camera Orthographic Size = 5.\n\n" +
            "This tool generates a cross-platform 1x1 solid block, the Player, and a Dual-Camera UI setup.",
            MessageType.Info);

        GUILayout.Space(15);

        if (GUILayout.Button("Generate Complete Level Architecture", GUILayout.Height(30)))
        {
            CreateLevel();
        }
    }

    private static void CreateLevel()
    {
        // 1. Environment Layers
        CreateLayerIfNeeded("Concrete");
        CreateLayerIfNeeded("Grass");
        CreateLayerIfNeeded("Player");

        float camHeight = OrthoSize * 2f;
        float camWidth = camHeight * AspectRatio;
        float panelWidth = camWidth / 2f;
        float panelHeight = GroundHeight;
        float panelCentreY = GroundBottomY + (panelHeight / 2f);
        float leftCentreX = -(panelWidth / 2f);
        float rightCentreX = (panelWidth / 2f);

        GameObject envRoot = new("Environment");

        GameObject concreteObj = CreateBaseGroundElement("Concrete Ground", new Vector2(leftCentreX, panelCentreY), new Vector2(panelWidth, panelHeight), new Color(0.5f, 0.5f, 0.5f), "Concrete");
        concreteObj.transform.SetParent(envRoot.transform);

        GameObject grassObj = Instantiate(concreteObj, envRoot.transform);
        grassObj.name = "Grass Ground";
        grassObj.transform.position = new Vector3(rightCentreX, panelCentreY, 0f);
        grassObj.layer = LayerMask.NameToLayer("Grass");
        grassObj.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.8f, 0.2f);

        // 2. Automate Player
        GameObject playerObj = CreatePlayer();

        // 3. Automate Dual-Camera & UI
        SetupCamerasAndCanvas();

        Undo.RegisterCreatedObjectUndo(envRoot, "Create Standard Level");
        Selection.activeGameObject = playerObj;

        Debug.Log($"[Helsinki Tools] Level, Player, and Dual-Camera UI generated perfectly.");
    }

    private static GameObject CreatePlayer()
    {
        GameObject player = new("Player");
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

    private static void SetupCamerasAndCanvas()
    {
        int uiLayer = LayerMask.NameToLayer("UI");

        // 1. MAIN CAMERA SETUP (The Base)
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            GameObject camObj = new("Main Camera");
            camObj.tag = "MainCamera";
            mainCam = camObj.AddComponent<Camera>();
            mainCam.orthographic = true;
            mainCam.orthographicSize = OrthoSize;
            Undo.RegisterCreatedObjectUndo(camObj, "Create Main Camera");
        }

        mainCam.cullingMask &= ~(1 << uiLayer);

        // Ensure Main Camera is set to Base
        UniversalAdditionalCameraData mainCamData = mainCam.GetUniversalAdditionalCameraData();
        mainCamData.renderType = CameraRenderType.Base;

        // 2. UI CAMERA SETUP (The Overlay)
        GameObject uiCamObj = new("UICamera");
        Camera uiCam = uiCamObj.AddComponent<Camera>();
        uiCam.orthographic = true;
        uiCam.orthographicSize = OrthoSize;
        uiCam.cullingMask = 1 << uiLayer;

        // Force URP Overlay Mode
        UniversalAdditionalCameraData uiCamData = uiCam.GetUniversalAdditionalCameraData();
        uiCamData.renderType = CameraRenderType.Overlay;

        // Inject the Overlay Camera into the Base Camera's Stack
        if (!mainCamData.cameraStack.Contains(uiCam))
        {
            mainCamData.cameraStack.Add(uiCam);
        }

        // Strip the redundant audio listener
        AudioListener extraListener = uiCamObj.GetComponent<AudioListener>();
        if (extraListener != null) Object.DestroyImmediate(extraListener);

        Undo.RegisterCreatedObjectUndo(uiCamObj, "Create UI Camera");

        // 3. CANVAS SETUP
        GameObject canvasObj = new GameObject("MainCanvas");
        canvasObj.layer = uiLayer; // MUST be on UI layer for the camera to see it

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = uiCam;
        canvas.planeDistance = 5f; // Push it slightly forward into the 2D orthographic view

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObj.AddComponent<GraphicRaycaster>();

        // 4. BACKGROUND PANEL SETUP
        GameObject panelObj = new("BackgroundPanel")
        {
            layer = uiLayer // Children must also be on the UI layer
        };
        panelObj.transform.SetParent(canvasObj.transform, false);

        Image panelImage = panelObj.AddComponent<Image>();
        panelImage.color = new Color(0.1f, 0.1f, 0.1f, 0.5f);

        RectTransform rt = panelObj.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        // 5. EVENT SYSTEM
        if (Object.FindAnyObjectByType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();

            // THE FIX: Forcing the modern Input System UI module
            eventSystem.AddComponent<InputSystemUIInputModule>();

            Undo.RegisterCreatedObjectUndo(eventSystem, "Create Event System");
        }

        Undo.RegisterCreatedObjectUndo(canvasObj, "Create Canvas");
    }

    private static GameObject CreateBaseGroundElement(string name, Vector2 position, Vector2 worldSize, Color color, string layerName)
    {
        GameObject obj = new(name);
        obj.transform.position = new(position.x, position.y, 0f);
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

        Texture2D tex = new(1, 1);
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