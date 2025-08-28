using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class AudioOcclusion : MonoBehaviour
{
    [Header("Listener (di solito la camera del player)")]
    public Transform listener;

    [Header("Occlusion Settings")]
    public LayerMask occlusionMask;
    public float occludedVolume = 0.3f;
    public float normalVolume = 1f;
    public float occludedCutoff = 800f;
    public float normalCutoff = 22000f;
    public float smoothTime = 5f;
    private AudioSource source;
    private AudioLowPassFilter lowPass;
    private float targetVolume;
    private float targetCutoff;

    void Start()
    {
        source = GetComponent<AudioSource>();
        lowPass = GetComponent<AudioLowPassFilter>();

        targetVolume = normalVolume;
        targetCutoff = normalCutoff;
        source.volume = normalVolume;
        lowPass.cutoffFrequency = normalCutoff;
    }

    void Update()
    {
        if (listener == null) return;

        Vector3 direction = listener.position - transform.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance, occlusionMask))
        {
            targetVolume = occludedVolume;
            targetCutoff = occludedCutoff;
        }
        else
        {
            targetVolume = normalVolume;
            targetCutoff = normalCutoff;
        }

        source.volume = Mathf.Lerp(source.volume, targetVolume, Time.deltaTime * smoothTime);
        lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, targetCutoff, Time.deltaTime * smoothTime);
    }
}
