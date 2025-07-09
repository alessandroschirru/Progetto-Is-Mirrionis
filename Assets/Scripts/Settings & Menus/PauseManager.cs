using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("Canvas da mostrare/attivare")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingsScreen;

    private bool isPaused = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseScreen.SetActive(isPaused);

        Time.timeScale = isPaused ? 1.0f : 0.0f;

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Settings()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }
}
