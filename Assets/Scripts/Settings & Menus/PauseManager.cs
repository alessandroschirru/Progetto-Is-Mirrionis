using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public static PauseManager instance;


    [Header("Canvas da mostrare/attivare")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingsScreen;
    [HideInInspector] static public bool isPaused = false;
    [HideInInspector] static public bool inPuzzle = false;
    public GameObject player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !LettersList.instance.letterOpen)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pauseScreen.SetActive(true);
            settingsScreen.SetActive(false);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
            pauseScreen.SetActive(false);
            settingsScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (inPuzzle == false)
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (inPuzzle == false)
        {
            Time.timeScale = 1f;
        }
    }

    public void Settings()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void BackToMainPause()
    {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }


    public void BackToMenu()
    {        
        SceneManager.LoadScene("MainMenu");
        pauseScreen.SetActive(false);
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
