using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Casermone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           SceneManager.LoadScene("InsideOldSchool");
        }
    }
}
