using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("");
    }


    public void QuitGame()
    {

      #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
      #else 
      Application.Quit();
      #endif

    }
}
