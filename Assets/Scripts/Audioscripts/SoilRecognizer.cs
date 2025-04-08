using UnityEngine;

public class SoilRecognizer : MonoBehaviour
{
    public Transform playerFeet; // Position to check for the layer under the player's feet
    public float checkRadius = 0.1f; // Radius of the check area
    public LayerMask groundLayerMask; // Layer mask to filter ground layers

    [System.Serializable] // Class to hold layer name and corresponding audio clip  
    public class LayerAudioPair
    {
        public string layerName;
        public AudioClip audioClip;
    }

    public LayerAudioPair[] layerAudioPairs; // List of layer names and corresponding audio clips

    private PlaySoundFromEvent playSoundFromEvent;
    private string currentLayerName;

    void Start()
    {
        playSoundFromEvent = GetComponent<PlaySoundFromEvent>();
        if (playSoundFromEvent == null)
        {
            playSoundFromEvent = gameObject.AddComponent<PlaySoundFromEvent>();
        }
    }

    void Update()
    {
        CheckLayerUnderFeet();
    }

    void CheckLayerUnderFeet()
    {
        Debug.Log($"Checking layer under feet at position: {playerFeet.position} with radius: {checkRadius}");
        Collider2D hit = Physics2D.OverlapCircle(playerFeet.position, checkRadius, groundLayerMask);
        if (hit != null)
        {
            string layerName = LayerMask.LayerToName(hit.gameObject.layer);
            Debug.Log($"Detected layer: {layerName}"); // Debug log to check detected layer
            if (layerName != currentLayerName)
            {
                currentLayerName = layerName;
                SwitchAudioClip(layerName);
            }
        }
        else
        {
            Debug.Log("No layer detected"); // Debug log when no layer is detected
        }
    }

    void SwitchAudioClip(string layerName)
    {
        foreach (var pair in layerAudioPairs)
        {
            if (pair.layerName == layerName)
            {
                playSoundFromEvent.soundClip = pair.audioClip;
                Debug.Log($"Switched audio clip to: {pair.audioClip.name} for layer: {layerName}"); // Debug log for switching audio clip
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (playerFeet != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerFeet.position, checkRadius);
        }
    }
}
