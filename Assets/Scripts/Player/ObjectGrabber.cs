using UnityEngine;
using UnityEngine.UI; 

public class ObjectGrabber : MonoBehaviour
{
    public float grabDistance = 3f;
    public LayerMask grabbableLayer;
    public float grabSmoothness = 10f;
    public float holdDistance = 1.5f;

    public Image crosshair; 
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.red;

    private Rigidbody grabbedObject = null;

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

        if (GetGrabbableObject(out _))
        {
            crosshair.color = highlightColor;
        }
        else
        {
            crosshair.color = defaultColor;
        }
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
}