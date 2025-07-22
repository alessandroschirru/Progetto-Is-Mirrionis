using UnityEngine;

public class MouseDrag2D : MonoBehaviour
{
    public LayerMask grabbableLayer;
    private Rigidbody2D grabbedRb;
    private Vector3 offset;
    private Transform initialTransform;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Mouse sinistro premuto
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast 2D per trovare l'oggetto grabbabile sotto il mouse
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0f, grabbableLayer);

            if (hit.collider != null)
            {
                grabbedRb = hit.collider.attachedRigidbody;
                if (grabbedRb != null)
                {
                    initialTransform = grabbedRb.transform;
                    offset = grabbedRb.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }

        if (Input.GetMouseButton(0) && grabbedRb != null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = mouseWorldPos + offset;
            targetPos.z = grabbedRb.transform.position.z; // mantieni il valore Z originale

            grabbedRb.MovePosition(targetPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            grabbedRb = null;

            // LOGICA RILASCIO MONETE

        }
    }
}