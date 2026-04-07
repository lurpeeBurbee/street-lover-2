using UnityEngine;

// 1. SACRED PATH: Automatically adds PlaySoundFromEvent if it's missing. No need to do it in Start().
[RequireComponent(typeof(PlaySoundFromEvent))]
public class SoilRecognizer : MonoBehaviour
{
    [Header("Detection Settings")]
    public Transform playerFeet;
    public float checkRadius = 0.1f;
    public LayerMask groundLayerMask;

    [Header("Audio Mapping")]
    public LayerAudioPair[] layerAudioPairs;

    [Header("Debug")]
    public bool showDebugLogs = false; // Toggle this in inspector to stop console spam

    // 2. Structs are generally lighter on memory than classes for simple data pairs like this
    [System.Serializable]
    public struct LayerAudioPair
    {
        public string layerName;
        public AudioClip audioClip;
    }

    private PlaySoundFromEvent playSoundFromEvent;
    private int currentLayerInt = -1; // Cache the layer integer for fast frame-by-frame checking. -1 means "no layer detected"

    void Start()
    {
        // Guaranteed to exist because of [RequireComponent]
        playSoundFromEvent = GetComponent<PlaySoundFromEvent>();
    }

    void Update()
    {
        CheckLayerUnderFeet();
    }

    void CheckLayerUnderFeet()
    {
        // 3. HOT PATH: Replaced the 'fat' circle with a perfectly thin Raycast pointing straight down
        RaycastHit2D hit = Physics2D.Raycast(playerFeet.position, Vector2.down, checkRadius, groundLayerMask);

        if (hit.collider != null)
        {
            // 4. MEMORY MAP: Check the integer first.
            if (hit.collider.gameObject.layer != currentLayerInt)
            {
                currentLayerInt = hit.collider.gameObject.layer;
                string newLayerName = LayerMask.LayerToName(currentLayerInt);

                if (showDebugLogs) Debug.Log($"Detected new soil layer: {newLayerName}");

                SwitchAudioClip(newLayerName);
            }
        }
        else
        {
            if (currentLayerInt != -1)
            {
                currentLayerInt = -1;
                if (showDebugLogs) Debug.Log("No layer detected (In Air)");
            }
        }
    }

    void SwitchAudioClip(string layerName)
    {
        foreach (var pair in layerAudioPairs)
        {
            if (pair.layerName == layerName)
            {
                playSoundFromEvent.soundClip = pair.audioClip;
                if (showDebugLogs) Debug.Log($"Switched audio to: {pair.audioClip.name} for {layerName}");
                break; // 6. Stop looping once we find the match to save CPU cycles
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (playerFeet != null)
        {
            Gizmos.color = Color.red;
            // Draw a line to accurately visualize the raycast
            Gizmos.DrawLine(playerFeet.position, playerFeet.position + Vector3.down * checkRadius);
        }
    }
}