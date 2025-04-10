using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class UIMickeyControl : MonoBehaviour
{
    [Header("FADE SETTINGS")]
    [Tooltip("Enable to fade in UI elements at start")]
    public bool fadeInOnStart = true;
    [Tooltip("Duration of fade-in animation (seconds)")]
    public float fadeInDuration = 1f;

    [Space(10)]
    [Tooltip("Enable scene transition fade-out")]
    public bool fadeOutOnExit = true;
    [Tooltip("Fade-out duration before loading new scene")]
    public float fadeOutDuration = 1f;
    [Tooltip("Drag a black panel covering the whole screen here")]
    public Image fadePanel;

    [Header("BUTTON SETTINGS")]
    [Tooltip("Main menu buttons")]
    public Button[] menuButtons = new Button[3];

    [Space(5)]
    [Tooltip("START BUTTON")]
    public string sceneToLoad = "MainScene";

    [Space(5)]
    [Tooltip("OPTIONS BUTTON")]
    public GameObject optionsCanvas;
    [Tooltip("Should options open as overlay?")]
    public bool optionsAsOverlay = true;

    private CanvasGroup canvasGroup;

    void Start()
    {
        // Set up canvas group if not using separate fade panel
        if (fadePanel == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = fadeInOnStart ? 0 : 1;
        }
        else
        {
            fadePanel.color = new Color(0, 0, 0, fadeInOnStart ? 0 : 1);
            fadePanel.gameObject.SetActive(fadeInOnStart);
        }

        optionsCanvas.SetActive(false); 

        // Initialize buttons
        if (menuButtons.Length >= 1 && menuButtons[0] != null) // Start
        {
            menuButtons[0].onClick.AddListener(() => StartCoroutine(LoadSceneAfterFade(sceneToLoad)));
        }

        if (menuButtons.Length >= 2 && menuButtons[1] != null) // Options
        {
            menuButtons[1].onClick.AddListener(ToggleOptions);
        }

        if (menuButtons.Length >= 3 && menuButtons[2] != null) // Quit
        {
            menuButtons[2].onClick.AddListener(QuitGame);
        }

        // Start fade in if enabled
        if (fadeInOnStart)
        {
            StartCoroutine(Fade(1, 0, fadeInDuration));
            //fadePanel.gameObject.SetActive(false);   
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);

            if (fadePanel != null)
            {
                fadePanel.color = new Color(0, 0, 0, alpha);
            }
            else if (canvasGroup != null)
            {
                canvasGroup.alpha = alpha;
            }

            yield return null;
        }
    }

    private IEnumerator LoadSceneAfterFade(string sceneName)
    {
        if (fadeOutOnExit)
        {
            yield return StartCoroutine(Fade(0, 1, fadeOutDuration));
        }
        SceneManager.LoadScene(sceneName);
    }

    private void ToggleOptions()
    {
        if (optionsCanvas != null)
        {
            optionsCanvas.SetActive(!optionsCanvas.activeSelf);

            // Disable other buttons when options are open
            foreach (var button in menuButtons)
            {
                if (button != menuButtons[1]) // Don't disable options button
                {
                    button.interactable = !optionsCanvas.activeSelf;
                }
            }
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
