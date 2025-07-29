using UnityEngine;
using UnityEngine.UI;

public class ObjectInteract : MonoBehaviour
{
    public float grabDistance = 3f;
    public LayerMask grabbableLayer;
    public float grabSmoothness = 10f;
    public float holdDistance = 1.5f;
    public Image crosshair;
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.red;
    private Rigidbody grabbedObject = null;
    private GameObject canvasReadable;
    public GameObject HintCanvas;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
            Debug.Log("Prova grab");
        }

        if (Input.GetMouseButtonUp(0) && grabbedObject != null)
        {
            ReleaseObject();
        }

        if (grabbedObject != null)
        {
            HoldObject();
        }

        if (Input.GetKeyDown(KeyCode.E) && canvasReadable != null && canvasReadable.activeSelf)
        {
            ClosePuzzleOrReadable();
        }

        else if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0f && !PauseManager.isPaused)
        {
            if (IsLookingAtDoor())
            {
                TryToggleDoor();
            }
            else if (IsLookingAtReadable())
            {
                TryInteractWithPuzzleOrReadable();
            }
            else if (isLookingAtTrapdoor())
            {
                TryToggleTrapdoor();
            }
        }

        UpdateCrosshair();
    }

    void TryGrabObject()
    {
        if (GetGrabbableObject(out Rigidbody targetRb))
        {
            grabbedObject = targetRb;
            grabbedObject.useGravity = false;
            grabbedObject.linearDamping = 10f;
        }
    }
    void HoldObject()
    {
        Vector3 holdPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
        Vector3 direction = holdPosition - grabbedObject.position;

        grabbedObject.linearVelocity = direction * grabSmoothness;
    }
    void ReleaseObject()
    {
        grabbedObject.useGravity = true;
        grabbedObject.linearDamping = 0f;
        grabbedObject = null;
        Debug.Log("Rilasciato");
    }
    void UpdateCrosshair()
    {
        if (crosshair == null) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            bool highlight = false;

            if ((grabbableLayer == (grabbableLayer | (1 << hit.collider.gameObject.layer))) || hit.collider.CompareTag("Readable"))
            {
                highlight = true;
            }

            if (hit.collider.CompareTag("Door") || hit.collider.CompareTag("Trapdoor"))
            {
                highlight = true;
                if (HintCanvas != null) HintCanvas.SetActive(true);
            }
            else
            {
                if(HintCanvas != null) HintCanvas.SetActive(false);
            }

                crosshair.color = highlight ? highlightColor : defaultColor;
            return;
        }

        crosshair.color = defaultColor;
        if (HintCanvas != null) HintCanvas.SetActive(false);
    }
    bool GetGrabbableObject(out Rigidbody rb)
    {
        rb = null;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayer))
        {
            if (hit.rigidbody != null)
            {
                rb = hit.rigidbody;
                return true;
            }
        }
        return false;
    }
    bool TryInteractWithPuzzleOrReadable()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Readable"))
            {
                InteractWithReadable(hit.collider.gameObject);
                return true;
            }
        }
        return false;
    }
    void InteractWithReadable(GameObject readable)
    {
        canvasReadable = readable.transform.GetChild(0).gameObject;
        canvasReadable.SetActive(true);
        PauseManager.inPuzzle = true; 
        crosshair.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    public void ClosePuzzleOrReadable()
    {
        canvasReadable.SetActive(false);
        PauseManager.inPuzzle = false;
        crosshair.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    bool TryToggleDoor()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            DoorHinge door = hit.collider.GetComponentInParent<DoorHinge>();
            if (door != null)
            {
                door.ToggleDoor();
                return true;
            }
        }
        return false;
    }

    bool TryToggleTrapdoor()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            TrapdoorHinge trapdoor = hit.collider.GetComponentInParent<TrapdoorHinge>();
            if(trapdoor != null)
            {
                trapdoor.ToggleTrapdoor();
                return true;
            }
        }
        return false;
    }

    bool IsLookingAtDoor()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        return Physics.Raycast(ray, out RaycastHit hit, grabDistance) && hit.collider.CompareTag("Door");
    }

    bool IsLookingAtReadable()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        return Physics.Raycast(ray, out RaycastHit hit, grabDistance) && hit.collider.CompareTag("Readable");
    }

    bool isLookingAtTrapdoor()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        return Physics.Raycast(ray, out RaycastHit hit, grabDistance) && hit.collider.CompareTag("Trapdoor");
    }
}