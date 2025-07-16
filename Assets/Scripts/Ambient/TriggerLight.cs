using UnityEngine;

public class TriggerLight : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    [SerializeField] private Color triggerColor = Color.green;
    [SerializeField] private Color originalColor = Color.red;
    private void Start()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        if (targetLight != null)
        {
            targetLight.color = originalColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Debug.Log("Nel Trigger");

            targetLight.color = triggerColor;
            
            Rigidbody rb = other.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePositionX|
                             RigidbodyConstraints.FreezePositionY|
                             RigidbodyConstraints.FreezePositionZ;
            rb.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.constraints = RigidbodyConstraints.FreezeRotation;


            other.transform.position = transform.position;
            other.gameObject.layer = LayerMask.NameToLayer("Default");
            Debug.Log($"Blocco in posizione!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Debug.Log("Fuori dal Trigger");
            targetLight.color = originalColor;
        }
    }
}
