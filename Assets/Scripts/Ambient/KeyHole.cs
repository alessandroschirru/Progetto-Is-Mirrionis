using UnityEngine;

public class KeyHole : MonoBehaviour
{
    public DoorHinge door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Debug.Log("Chiave inserita");

            other.transform.position = transform.position;
            other.transform.rotation =transform.rotation;
            other.transform.SetParent(transform);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            
            other.gameObject.layer = LayerMask.NameToLayer("Default");
            Debug.Log($"Porta sbloccata!");
        }

        if(door != null)
        {
            door.isLocked = false;
        }

        else
        {
            Debug.LogWarning("Nessuna porta collegata al KeyHole!");
        }
    }

}
