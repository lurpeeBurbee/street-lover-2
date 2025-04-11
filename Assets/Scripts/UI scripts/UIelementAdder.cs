using UnityEngine;
using UnityEngine.UI;

public class UIElementAdder : MonoBehaviour
{
    [Header("Layout Group Targets")]
    [Tooltip("Left horizontal layout group")]
    public HorizontalLayoutGroup leftGroup;
    [Tooltip("Right horizontal layout group")]
    public HorizontalLayoutGroup rightGroup;

    [Header("Addition Settings")]
    [Tooltip("Add to left group when pressing space?")]
    public bool addToLeft = true;
    [Tooltip("Add to right group when pressing space?")]
    public bool addToRight = true;
    [Tooltip("Size of the red squares")]
    public float squareSize = 50f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddSquares();
        }
    }

    private void AddSquares()
    {
        if (addToLeft && leftGroup != null)
        {
            CreateSquare(leftGroup.transform);
        }

        if (addToRight && rightGroup != null)
        {
            CreateSquare(rightGroup.transform);
        }
    }

    private void CreateSquare(Transform parent)
    {
        // Create new GameObject
        GameObject square = new("RedSquare", typeof(Image));

        // Set parent and make it first in hierarchy (so it appears leftmost)
        square.transform.SetParent(parent);
        square.transform.SetAsFirstSibling();

        // Configure Image component
        Image img = square.GetComponent<Image>();
        img.color = Color.red;

        // Add LayoutElement to control sizing
        LayoutElement layout = square.AddComponent<LayoutElement>();
        layout.preferredWidth = squareSize;
        layout.preferredHeight = squareSize;
        layout.flexibleWidth = 0.1f; // Allows slight squeezing when crowded
        layout.flexibleHeight = 0.1f;
    }
}