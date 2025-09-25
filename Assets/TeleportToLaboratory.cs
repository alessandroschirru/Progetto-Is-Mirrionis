using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToLaboratory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("LaboratoryScene");
        }
    }
}
