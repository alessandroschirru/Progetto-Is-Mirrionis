using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToLazarus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("LazarusScene");
        }
    }
}