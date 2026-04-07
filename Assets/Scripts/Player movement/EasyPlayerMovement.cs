using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class EasyPlayerMovement : MonoBehaviour
{
    [Header("Input Settings")]
    [Tooltip("Local input action for horizontal movement. If left empty, defaults to A/D and Left/Right Arrows.")]
    public InputAction moveAction;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;

    private Rigidbody2D rb;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure the character doesn't tip over like a domino
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // THE AUTOMATION: If the inspector slot is completely empty, build the default bindings via code.
        if (moveAction == null || moveAction.bindings.Count == 0)
        {
            // Initialize a value-type action expecting a 1D axis (float)
            moveAction = new InputAction(type: InputActionType.Value, expectedControlType: "Axis");

            // Generate the A/D keys composite
            moveAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Keyboard>/a")
                .With("Positive", "<Keyboard>/d");

            // Generate the Left/Right Arrow keys composite
            moveAction.AddCompositeBinding("1DAxis")
                .With("Negative", "<Keyboard>/leftArrow")
                .With("Positive", "<Keyboard>/rightArrow");
        }
    }

    private void OnEnable()
    {
        // Local actions must be manually enabled to listen to hardware
        moveAction.Enable();
    }

    private void OnDisable()
    {
        // Always clean up memory when the object turns off
        moveAction.Disable();
    }

    private void Update()
    {
        // 1. VISUAL/LOGIC CLOCK: Read the float value (-1 for left, 1 for right, 0 for still)
        moveInput = moveAction.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        // 2. PHYSICS CLOCK: Apply the speed directly to the Rigidbody's linear velocity
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}