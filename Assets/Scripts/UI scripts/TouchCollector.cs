using UnityEngine;

public class TouchCollector : MonoBehaviour
{
    [SerializeField] private UItoolkitControl uiControl;
    private Camera mainCam;

    void Start() => mainCam = Camera.main;

    void Update()
    {
        // Detects first touch or mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new(mousePos.x, mousePos.y);

            // Cast a ray to see if we hit a 2D collider (the barrel)
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("PaleAleHealth"))
            {
                // Trigger the UI update and disable the barrel
                uiControl.BarrelControl();
                hit.collider.gameObject.SetActive(false);
            }
        }
    }
}