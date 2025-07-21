using UnityEngine;

public class BuildingPuzzle : MonoBehaviour
{
    [Header("Controlli incastro")]
    [SerializeField] private string expectedBlockName = "Block-1";
    [Header("Azioni")]
    [SerializeField] private bool lockBlock = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Block")) return;

        if (!other.name.Equals(expectedBlockName))
        {
            Debug.Log($"[{other.name}] non va in [{gameObject.name}]");
            return;
        }

        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.constraints = lockBlock ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.FreezePosition;
        }

        other.gameObject.layer = LayerMask.NameToLayer("Default");
    }
}