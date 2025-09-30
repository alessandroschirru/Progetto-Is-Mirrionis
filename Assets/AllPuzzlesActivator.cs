using UnityEngine;
using System.Linq;

public class AllPuzzlesActivator : MonoBehaviour
{
    [Header("Puzzle richiesti")]
    public string[] puzzleIDs;          // metti qui i 4 ID

    [Header("Cosa attivare alla fine")]
    public GameObject targetToActivate; // mettilo disattivo in scena
    public bool deactivateIfNotAll = false; // opzionale: spegne se non più tutti completi

    void Start()
    {
        Evaluate();
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.PuzzleCompleted += OnPuzzleCompleted;
    }

    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.PuzzleCompleted -= OnPuzzleCompleted;
    }

    void OnPuzzleCompleted(string id)
    {
        // Ogni volta che un puzzle si completa, ricontrolla
        Evaluate();
    }

    void Evaluate()
    {
        if (targetToActivate == null || GameStateManager.Instance == null) return;

        bool allDone = puzzleIDs != null && puzzleIDs.Length > 0 &&
                       puzzleIDs.All(pid => GameStateManager.Instance.IsPuzzleCompleted(pid));

        if (allDone) targetToActivate.SetActive(true);
        else if (deactivateIfNotAll) targetToActivate.SetActive(false);
    }
}