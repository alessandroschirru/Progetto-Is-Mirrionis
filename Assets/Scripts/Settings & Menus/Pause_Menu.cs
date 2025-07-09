using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject Overlay;
    public GameObject Player;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (isPaused)
            {
                ResumeGame();
            }

            else
            {
                PauseGame();
            }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Overlay.SetActive(false);
        Player.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Overlay.SetActive(true);
        Player.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
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
