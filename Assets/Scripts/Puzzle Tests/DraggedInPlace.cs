using UnityEngine;

public class DraggedInPlace : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Draggable"))
        {
            ObjectDrag draggable = collision.gameObject.GetComponent<ObjectDrag>();
            if (draggable != null && draggable.IsDragging())
            {
                draggable.StopDragging();

                collision.gameObject.tag = "Untagged";

                Debug.Log($"{collision.gameObject.name} fermato dallo stopper e tag cambiata in Untagged");
            }
        }
    }
}