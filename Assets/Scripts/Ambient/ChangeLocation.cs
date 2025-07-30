using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLocation : MonoBehaviour
{
    [SerializeField] private TrapdoorHinge trapdoor;
    [SerializeField] private string sceneName;

    private void Update()
    {
        Debug.Log("Trapdoor: " + trapdoor.isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} è entrato nel trigger");

        if (other.CompareTag("Player") && trapdoor != null && trapdoor.isOpen)
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("Cambio Scena");
        }
    }
}
