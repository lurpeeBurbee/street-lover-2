using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UItoolkitControl : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement mainInfo;

    // Cache the list of icons to avoid the Repaint error
    private List<VisualElement> barrelIcons = new List<VisualElement>();
    private int collectionCount = 0;

    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        // Find the container and immediately hide all children logic-side
        var itemsContainer = root.Q<VisualElement>("Items");
        if (itemsContainer != null)
        {
            foreach (var child in itemsContainer.Children())
            {
                barrelIcons.Add(child);
                child.style.display = DisplayStyle.None; // Start hidden
            }
        }

        mainInfo = root.Q<VisualElement>("main");
        if (mainInfo != null) mainInfo.style.display = DisplayStyle.None;
    }

    public void BarrelControl()
    {
        // 1. Show the specific icon from our cached list
        if (collectionCount < barrelIcons.Count)
        {
            barrelIcons[collectionCount].style.display = DisplayStyle.Flex;
            collectionCount++;
        }

        // 2. Info Screen logic
        if (mainInfo != null && mainInfo.style.display != DisplayStyle.Flex)
        {
            StopAllCoroutines(); // Reset if clicking fast
            StartCoroutine(ShowInfoScreen());
        }
    }

    IEnumerator ShowInfoScreen()
    {
        mainInfo.style.display = DisplayStyle.Flex;
        yield return new WaitForSeconds(1.5f);
        mainInfo.style.display = DisplayStyle.None;
    }
}