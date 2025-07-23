using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 startPosition;

    private void OnMouseDown()
    {
        this.transform.GetComponent<CircleCollider2D>().enabled = true;
        isDragging = true;

        startPosition = transform.position;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
        Debug.Log("preso");
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        this.transform.GetComponent<CircleCollider2D>().enabled = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if(hit.transform == null)
        {
            transform.position = startPosition;
        }
        else if(hit.transform.tag == "CoinSpot")
        {
            transform.position = hit.transform.position;
        }

        this.transform.GetComponent<CircleCollider2D>().enabled = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
