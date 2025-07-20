using UnityEngine;

public class DoorHinge : MonoBehaviour
{
    public GameObject doorPivot;
    public GameObject interactionCanvas;
    public Collider doorCollider;
    public float openAngle = 90f;
    public float rotationSpeed = 2f;

    public bool isLocked = true;
    private bool isPlayerInRange = false;
    private bool isDoorOpen = false;
    private bool isAnimating = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (doorPivot != null)
        {
            initialRotation = doorPivot.transform.localRotation;
        }

        if(interactionCanvas != null)
        {
            interactionCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }

        if (isAnimating && doorPivot != null)
        {
            if(doorCollider != null)
            {
                doorCollider.enabled = false;
            }

            doorPivot.transform.localRotation = Quaternion.Slerp(doorPivot.transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(doorPivot.transform.localRotation, targetRotation) < 0.1f)
            {
                doorPivot.transform.localRotation = targetRotation;
                isAnimating = false;

                if(doorCollider != null)
                {
                    doorCollider.enabled = true;
                }
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if(interactionCanvas != null)
            {
                interactionCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if(interactionCanvas != null)
            {
                interactionCanvas.SetActive(false);
            }
        }
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
