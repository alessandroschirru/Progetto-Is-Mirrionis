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
            }
        }
    }

    public void ToggleTrapdoor()
    {
        if (isLocked || isAnimating || hingePivot == null) return;

        isOpen = !isOpen;
        float angle = isOpen ? openAngle : 0f;

        targetRotation = initialRotation * Quaternion.Euler(0f, 0f, angle);
        isAnimating = true;
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
