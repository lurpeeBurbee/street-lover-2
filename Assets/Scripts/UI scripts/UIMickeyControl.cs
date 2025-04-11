using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIMickeyControl : MonoBehaviour
{
    [Header("FADE SETTINGS")]
    public bool fadeInOnStart = true;
    public float fadeInDuration = 1f;

    [Space(10)]
    public bool fadeOutOnExit = true;
    public float fadeOutDuration = 1f;
    // Assign a UI Image Panel here (e.g., a black image covering the screen)
    public Image fadePanel;

    [Header("BUTTON SETTINGS")]
    // Assign your menu buttons (Start, Options, Quit)
    public Button[] menuButtons = new Button[3];

    [Space(5)]
    public string sceneToLoad = "MainScene"; // Scene to load on Start button press

    [Space(5)]
    // Assign the GameObject containing your Options UI elements
    public GameObject optionsCanvas;
    // If true, main menu buttons are disabled when options are open.
    // If false, main menu buttons remain enabled even when options are open.
    public bool optionsAsOverlay = true;

    // Used if fadePanel is not assigned, fades the whole Canvas this script is on
    private CanvasGroup canvasGroup;

    void Start()
    {
        // --- Fade Element Initialization ---
        if (fadePanel == null)
        {
            // Attempt to get or add a CanvasGroup if no Image panel is provided
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                Debug.LogWarning("UIMickeyControl: No Fade Panel assigned, adding CanvasGroup to GameObject.", this);
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            // Set initial alpha: 1 (opaque) if fading in, 0 (transparent) otherwise
            canvasGroup.alpha = fadeInOnStart ? 1f : 0f;
            // Ensure interaction is blocked initially if fading in
            canvasGroup.blocksRaycasts = fadeInOnStart;
        }
        else
        {
            // Using an Image Panel for fading
            Color panelColor = fadePanel.color;
            panelColor.a = fadeInOnStart ? 1f : 0f;
            fadePanel.color = panelColor;
            fadePanel.gameObject.SetActive(fadeInOnStart || fadeOutOnExit);
            fadePanel.raycastTarget = fadeInOnStart;
        }

        // --- Options Canvas Initialization ---
        if (optionsCanvas != null)
        {
            optionsCanvas.SetActive(false);
        }
        else
        {
            if (menuButtons.Length >= 2 && menuButtons[1] != null)
            {
                Debug.LogWarning("UIMickeyControl: Options Button assigned, but Options Canvas is not.", this);
            }
        }


        // --- Button Listener Setup ---
        if (menuButtons.Length >= 1 && menuButtons[0] != null)
            menuButtons[0].onClick.AddListener(() => StartCoroutine(LoadSceneAfterFade(sceneToLoad)));
        else
            Debug.LogWarning("UIMickeyControl: Start Button (menuButtons[0]) not assigned.", this);

        if (menuButtons.Length >= 2 && menuButtons[1] != null)
            menuButtons[1].onClick.AddListener(ToggleOptions);
        else
            Debug.LogWarning("UIMickeyControl: Options Button (menuButtons[1]) not assigned.", this);

        if (menuButtons.Length >= 3 && menuButtons[2] != null)
            menuButtons[2].onClick.AddListener(QuitGame);
        else
            Debug.LogWarning("UIMickeyControl: Quit Button (menuButtons[2]) not assigned.", this);

        // --- Start Fade-In ---
        if (fadeInOnStart)
        {
            StartCoroutine(Fade(1f, 0f, fadeInDuration));
        }

        // --- Initial Button State ---
        SetButtonsInteractable(optionsCanvas == null || !optionsCanvas.activeSelf);
    }

    /// <summary>
    /// Coroutine to fade the fadePanel or canvasGroup alpha over time.
    /// </summary>
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        // Ensure the fade element is active and ready
        if (fadePanel != null)
        {
            if (!fadePanel.gameObject.activeSelf) fadePanel.gameObject.SetActive(true);
            fadePanel.raycastTarget = true; // Block raycasts during fade
            Color panelColor = fadePanel.color;
            panelColor.a = startAlpha;
            fadePanel.color = panelColor;
        }
        else if (canvasGroup != null)
        {
            canvasGroup.alpha = startAlpha;
            canvasGroup.blocksRaycasts = true; // Block raycasts during fade
        }
        else
        {
            yield break; // Exit if no fade element is available
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);

            // Update alpha
            if (fadePanel != null)
            {
                Color panelColor = fadePanel.color;
                panelColor.a = currentAlpha;
                fadePanel.color = panelColor;
            }
            else if (canvasGroup != null)
            {
                canvasGroup.alpha = currentAlpha;
            }

            yield return null;
        }

        // Ensure the final alpha value is set precisely
        if (fadePanel != null)
        {
            Color panelColor = fadePanel.color;
            panelColor.a = endAlpha;
            fadePanel.color = panelColor;
            fadePanel.raycastTarget = (endAlpha > 0.01f);
            if (endAlpha < 0.01f && !fadeOutOnExit)
            {
                fadePanel.gameObject.SetActive(false);
            }
        }
        else if (canvasGroup != null)
        {
            canvasGroup.alpha = endAlpha;
            canvasGroup.blocksRaycasts = (endAlpha > 0.01f);
        }
    }

    /// <summary>
    /// Coroutine to handle fading out (if enabled) and then loading a new scene.
    /// </summary>
    private IEnumerator LoadSceneAfterFade(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("UIMickeyControl: Scene name to load is empty!", this);
            yield break;
        }

        if (fadeOutOnExit)
        {
            yield return StartCoroutine(Fade(0f, 1f, fadeOutDuration));
        }

        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Toggles the visibility of the options canvas and updates button interactability accordingly.
    /// </summary>
    private void ToggleOptions()
    {
        if (optionsCanvas != null)
        {
            // Determine the NEW state the options canvas will be in
            bool isShowingOptions = !optionsCanvas.activeSelf;
            // Apply the new state
            optionsCanvas.SetActive(isShowingOptions);

            // --- UPDATED LOGIC ---
            // Determine the desired state for the main menu buttons.
            // Buttons should be interactable UNLESS options are showing AND it's configured as an overlay.
            bool shouldButtonsBeInteractable = !(isShowingOptions && optionsAsOverlay);

            // Update the button states based on the calculated state
            SetButtonsInteractable(shouldButtonsBeInteractable);
        }
        else
        {
            Debug.LogError("UIMickeyControl: Cannot toggle options - Options Canvas is not assigned.", this);
        }
    }

    /// <summary>
    /// Sets the interactable state of all assigned menu buttons.
    /// </summary>
    private void SetButtonsInteractable(bool state)
    {
        if (menuButtons == null) return;

        foreach (var button in menuButtons)
        {
            if (button != null)
                button.interactable = state;
        }
    }

    /// <summary>
    /// Quits the application or stops play mode in the editor.
    /// </summary>
    private void QuitGame()
    {
        Debug.Log("UIMickeyControl: QuitGame called.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
