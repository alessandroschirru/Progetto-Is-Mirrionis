using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 startPosition;
    private bool isDragging = false;

    // Layer da considerare per il drag (solo le monete)
    public LayerMask draggableLayer;

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Inizio drag
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, draggableLayer);

            if (hit != null && hit.gameObject == gameObject)
            {
                StartDrag(mouseWorldPos);
            }
        }

        // Durante drag
        if (isDragging && Input.GetMouseButton(0))
        {
            Drag(mouseWorldPos);
        }

        // Fine drag
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            EndDrag(mouseWorldPos);
        }
    }

    void StartDrag(Vector2 mousePos)
    {
        startPosition = transform.position;
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
        isDragging = true;
    }

    void Drag(Vector2 mousePos)
    {
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
    }

    void EndDrag(Vector2 mousePos)
    {
        isDragging = false;

        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

        bool onCoinSpot = false;
        bool onTable = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("CoinSpot"))
            {
                transform.position = hit.transform.position;
                onCoinSpot = true;
                break;
            }
            else if (hit.CompareTag("Table"))
            {
                onTable = true;
            }
        }

        if (!onCoinSpot && !onTable)
        {
            transform.position = startPosition;
        }
    }
}