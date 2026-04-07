using UnityEngine;
[RequireComponent(typeof(PlaySoundFromEvent))]
public class EasySoilRecognizer : MonoBehaviour
{
    [System.Serializable]
    public struct SoilAudio
    {
        [Tooltip("Use the dropdown to select the layer (e.g., Grass, Stone)")]
        public LayerMask soilLayer; // Replaced 'string' with a dropdown!
        public AudioClip clip;
    }

    [Header("Detection Settings")]
    public Transform playerFeet;
    public float checkRadius = 0.1f;
    public LayerMask validGroundLayers; // Use this to ensure the raycast ignores the player's body

    [Header("Audio Mapping")]
    public SoilAudio[] soilTypes;

    private PlaySoundFromEvent audioPlayer;
    private int lastLayerHit = -1;

    private void Awake()
    {
        // Awake is safer than Start for caching components
        audioPlayer = GetComponent<PlaySoundFromEvent>();
    }

    private void FixedUpdate()
    {
        // 1. HOT PATH: Run strictly on the physics clock
        RaycastHit2D hit = Physics2D.Raycast(playerFeet.position, Vector2.down, checkRadius, validGroundLayers);

        if (hit.collider != null)
        {
            int currentLayer = hit.collider.gameObject.layer;

            // 2. MEMORY MAP: Only search for a new sound if the terrain physically changed
            if (currentLayer != lastLayerHit)
            {
                lastLayerHit = currentLayer;
                UpdateFootstepSound(currentLayer);
            }
        }
        else
        {
            lastLayerHit = -1; // We are in the air
        }
    }

    private void UpdateFootstepSound(int layerIndex)
    {
        foreach (var soil in soilTypes)
        {
            // 3. THE "HELSINKI HACK": Bitwise check to see if the integer is inside the LayerMask
            if ((soil.soilLayer.value & (1 << layerIndex)) > 0)
            {
                audioPlayer.soundClip = soil.clip;
                break; // Stop looping to save CPU cycles
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (playerFeet != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(playerFeet.position, playerFeet.position + Vector3.down * checkRadius);
        }
    }
}
