using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private bool isDragging;
    [SerializeField] private Transform dragTarget;
    public AudioSource dragAudioSource;
    public AudioClip dragClip;
    public float dragVolume = 0.5f;

    private bool isPlayingDrag = false;

    public float dragForce = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    public void StartDragging()
    {
        isDragging = true;
        rb.useGravity = true;
        rb.linearDamping = 5f;
    }

    public void StopDragging()
    {
        isDragging = false;
        rb.linearDamping = 0f;

        if (isPlayingDrag && dragAudioSource != null)
        {
            dragAudioSource.Stop();
            isPlayingDrag = false;
        }
    }

    public void ContinueDragging()
    {
        if (!isDragging)
        {
            if (isPlayingDrag)
            {
                dragAudioSource.Stop();
                isPlayingDrag = false;
            }
            return;
        }

        Vector3 targetPos = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        Vector3 dir = targetPos - rb.position;

        rb.AddForce(dir * dragForce, ForceMode.Force);

        // Gestione audio
        if (!isPlayingDrag && dragAudioSource != null && dragClip != null)
        {
            dragAudioSource.clip = dragClip;
            dragAudioSource.loop = true;
            dragAudioSource.volume = dragVolume;
            dragAudioSource.Play();
            isPlayingDrag = true;
        }
    }
    public bool IsDragging() => isDragging;
}