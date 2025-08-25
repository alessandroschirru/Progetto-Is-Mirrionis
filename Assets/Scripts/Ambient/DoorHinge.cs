using UnityEngine;

public class DoorHinge : MonoBehaviour
{
    public GameObject doorPivot;
    public GameObject interactionCanvas;
    public float openAngle = 90f;
    public float rotationSpeed = 2f;

    public bool isLocked = true;
    private bool isDoorOpen = false;
    private bool isAnimating = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public AudioSource doorAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (doorPivot != null)
        {
            initialRotation = doorPivot.transform.localRotation;
        }

        isLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating && doorPivot != null)
        {
            doorPivot.transform.localRotation = Quaternion.Slerp(
                doorPivot.transform.localRotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );

            if (Quaternion.Angle(doorPivot.transform.localRotation, targetRotation) < 0.1f)
            {
                doorPivot.transform.localRotation = targetRotation;
                isAnimating = false;
            }
        }
    }


    public void ToggleDoor()
    {
        if (isLocked || isAnimating) return;

        isDoorOpen = !isDoorOpen;
        float targetY = isDoorOpen ? openAngle : 0f;
        targetRotation = Quaternion.Euler(0f, targetY, 0f);
        isAnimating = true;

        if(doorAudioSource != null)
        {
            doorAudioSource.PlayOneShot(doorAudioSource.clip);
        }

    }

    public void Unlock()
    {
        isLocked = false;

        Debug.Log("Porta Sbloccata");
    }
}
