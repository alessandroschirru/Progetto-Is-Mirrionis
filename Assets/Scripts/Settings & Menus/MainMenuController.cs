using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuScreen;
    [SerializeField] private GameObject settingsScreen;

    private void Start()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Outside");
    }

    public void OpenSettings()
    {
        MainMenuScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void SaveSettingsAndBack()
    {
        MainMenuScreen.SetActive(true);
        settingsScreen.SetActive(false);
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
