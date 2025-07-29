using UnityEngine;

public class DraggableBlock : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 originalPosition;
    private Camera cam;

    private bool isDragging = false;
    private bool isMoving = false;
    private Vector3 targetPosition;

    public float gridCellSize = 1.0f;
    public float moveSpeed = 10f;
    public LayerMask obstacleLayer;
    public float dragThreshold = 0.4f;

    public bool isDraggable = true;

    private void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        if (isMoving) return; // evita nuovi movimenti mentre uno è in corso

        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        originalPosition = transform.position;
    }

    void OnMouseDrag()
    {
        if (!isDraggable || !isDragging || isMoving) return;

        Vector3 currentMousePos = GetMouseWorldPosition() + offset;
        Vector3 dragVector = currentMousePos - originalPosition;

        if (dragVector.magnitude < dragThreshold) return;

        Vector3 direction = Vector3.zero;

        // Direzione dominante (asse X o Y)
        if (Mathf.Abs(dragVector.x) > Mathf.Abs(dragVector.y))
            direction = (dragVector.x > 0) ? Vector3.right : Vector3.left;
        else
            direction = (dragVector.y > 0) ? Vector3.up : Vector3.down;

        if (CanMoveInDirection(direction))
        {
            targetPosition = originalPosition + direction * gridCellSize * 2f;
            StartCoroutine(MoveToTarget(targetPosition));
            isDragging = false; // blocca ulteriori drag finché non rilascio
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private System.Collections.IEnumerator MoveToTarget(Vector3 target)
    {
        isMoving = true;
        while ((transform.position - target).sqrMagnitude > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        originalPosition = target;
        isMoving = false;
    }

    bool CanMoveInDirection(Vector3 direction)
    {
        float rayLength = gridCellSize;
        Vector3 rayOrigin = transform.position + direction * 0.05f;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayLength, obstacleLayer);
        Debug.DrawRay(rayOrigin, direction * rayLength, Color.red, 1f);

        return hit.collider == null;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        return cam.ScreenToWorldPoint(mouseScreenPos);
    }
}