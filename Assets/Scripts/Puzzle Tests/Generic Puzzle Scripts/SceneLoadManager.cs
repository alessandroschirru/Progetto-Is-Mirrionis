using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance;
    [HideInInspector] public string puzzleToLoad;
    private GameObject player;

    [HideInInspector] public GameObject puzzle;

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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "PuzzleScene")
        {
            if (player.activeSelf)
            {
                player.SetActive(false);
            }
        } 

        if (SceneManager.GetActiveScene().name == "LaboratoryScene")
        {
            if (!player.activeSelf)
            {
                player.SetActive(true);
            }
        } 
    }

    public void LoadPuzzleScene(string puzzleID)
    {
        puzzleToLoad = puzzleID;
        SceneManager.LoadScene("PuzzleScene");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsurePlayerRef();

        if (scene.name == "PuzzleScene")
        {
            if (player != null && player.activeSelf) player.SetActive(false);

            GameObject puzzlesRoot = GameObject.Find("Puzzles");
            if (puzzlesRoot == null) { Debug.LogError("PuzzlesRoot not found in PuzzleScene."); return; }

            puzzle = FindInChildren(puzzlesRoot.transform, puzzleToLoad);
            if (puzzle != null) puzzle.SetActive(true);
            else Debug.LogWarning("Puzzle not found: " + puzzleToLoad);
        }
        else if (scene.name == "LaboratoryScene")
        {
            if (player != null && !player.activeSelf) player.SetActive(true);
            RestorePlayerTransformSafe(); // <<< qui rimetti posizione/rotazione
        }
    }

    void EnsurePlayerRef()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
    }

    void RestorePlayerTransformSafe()
    {
        if (player == null || GameStateManager.Instance == null) return;

        if (GameStateManager.Instance.TryGetSavedPlayerState(out var pos, out var rot))
        {
            var cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            var rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                bool wasKinematic = rb.isKinematic;
                rb.isKinematic = true;
                player.transform.SetPositionAndRotation(pos, rot);
                rb.isKinematic = wasKinematic;
            }
            else
            {
                player.transform.SetPositionAndRotation(pos, rot);
            }

            if (cc != null) cc.enabled = true;

            GameStateManager.Instance.ClearSavedPlayerState();
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