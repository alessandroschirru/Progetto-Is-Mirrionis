using UnityEngine;

public class TrapdoorHinge : MonoBehaviour
{
    public GameObject hingePivot;
    public GameObject InteractionCanvas;
    public float openAngle = 90f;
    public float rotationSpeed = 2f;

    public bool isLocked = true;
    public bool isOpen = false;
    private bool isAnimating = false;

    Quaternion initialRotation;
    Quaternion targetRotation;

    public AudioSource TrapdoorAudioSource;
    public AudioClip openingClip;
    public AudioClip unlockingClip;
    public AudioClip lockedClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(hingePivot != null)
        {
            initialRotation = hingePivot.transform. localRotation;
        }

        else
        {
            Debug.LogError($"[{gameObject.name}] hingePivot non assegnato!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimating && hingePivot != null)
        {
            hingePivot.transform.localRotation = Quaternion.Slerp(
                hingePivot.transform.localRotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
                );

            if(Quaternion.Angle(hingePivot.transform.localRotation, targetRotation) < 0.1f)
            {
                hingePivot.transform.localRotation = targetRotation;
                isAnimating = false;
                isOpen = Mathf.Approximately(targetRotation.eulerAngles.z, openAngle);
            }
        }
    }

    public void ToggleTrapdoor()
    {
        if (isAnimating || hingePivot == null) return;

        if (isLocked)
        {
            TrapdoorAudioSource.PlayOneShot(lockedClip);
            return;
        }

        float startZ = hingePivot.transform.localRotation.eulerAngles.z;
        float targetZ = isOpen ? 0f : openAngle;

        targetRotation = Quaternion.Euler(
            initialRotation.eulerAngles.x,
            initialRotation.eulerAngles.y,
            targetZ
            );

        isAnimating = true;

        TrapdoorAudioSource.PlayOneShot(openingClip);
    }

    public void Unlock()
    {
        isLocked = false;
        TrapdoorAudioSource.PlayOneShot(unlockingClip);
    }
}
