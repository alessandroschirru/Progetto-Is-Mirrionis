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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        if (Input.GetMouseButtonUp(0) && grabbedObject != null)
        {
            ReleaseObject();
        }

        if (grabbedObject != null)
        {
            HoldObject();
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0f)
        {
            TryInteractWithPuzzleOrReadable();
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && canvasReadable && !PauseManager.isPaused)
        {
            ClosePuzzleOrReadable();
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
    }
    void UpdateCrosshair()
    {
        if (crosshair == null) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if ((grabbableLayer == (grabbableLayer | (1 << hit.collider.gameObject.layer))) || hit.collider.CompareTag("Readable"))
            {
                crosshair.color = highlightColor;
                return;
            }
        }

        crosshair.color = defaultColor;
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
    void TryInteractWithPuzzleOrReadable()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Readable"))
            {
                InteractWithReadable(hit.collider.gameObject);
            }
        }
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
}