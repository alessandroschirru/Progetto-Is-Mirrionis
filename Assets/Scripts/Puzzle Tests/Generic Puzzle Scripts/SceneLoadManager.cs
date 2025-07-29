using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance;
    public string puzzleToLoad;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste tra le scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadPuzzleScene(string puzzleID)
    {
        puzzleToLoad = puzzleID;
        SceneManager.LoadScene("PuzzleScene");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PuzzleScene")
        {
            GameObject puzzlesRoot = GameObject.Find("Puzzles");
            if (puzzlesRoot == null)
            {
                Debug.LogError("PuzzlesRoot not found in PuzzleRoom scene.");
                return;
            }

            GameObject puzzle = FindInChildren(puzzlesRoot.transform, puzzleToLoad);
            if (puzzle != null)
            {
                puzzle.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Puzzle not found: " + puzzleToLoad);
            }
        }
    }

    // Ricerca ricorsiva nei figli, anche se disattivati
    GameObject FindInChildren(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child.gameObject;

            GameObject result = FindInChildren(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
}