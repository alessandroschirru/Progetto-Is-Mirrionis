using UnityEngine;

public class PairedPuzzle3D : MonoBehaviour
{
    [Header("ID del puzzle (uguale a PairedPuzzle.puzzleID)")]
    public string puzzleID;

    [Header("Oggetti della scena 3D legati a questo puzzle")]
    public GameObject triggerSphere;      // la sfera/trigger con tag Readable (o il suo parent)
    public GameObject[] objectsToEnable;  // i 2 tubi (o più) da attivare quando completato

    void Start()
    {
        ApplyState();
    }

    public void ApplyState()
    {
        bool done = GameStateManager.Instance != null
                    && GameStateManager.Instance.IsPuzzleCompleted(puzzleID);

        if (done)
        {
            if (triggerSphere != null) Destroy(triggerSphere);
            if (objectsToEnable != null)
            {
                foreach (var go in objectsToEnable)
                    if (go) go.SetActive(true);
            }
        }
        // Se non è completato, non tocco nulla: sfera rimane, tubi rimangono off
    }
}
