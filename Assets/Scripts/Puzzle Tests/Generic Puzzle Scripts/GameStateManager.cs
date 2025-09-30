using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public Vector3 playerSavedPosition;
    public Quaternion playerSavedRotation;
    public bool hasSavedPosition = false;
    public event System.Action<string> PuzzleCompleted;

    private HashSet<string> completedPuzzles = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsPuzzleCompleted(string id)
    {
        return !string.IsNullOrEmpty(id) && completedPuzzles.Contains(id);
    }

    public void MarkPuzzleAsCompleted(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        if (completedPuzzles.Add(id))
        {
            Debug.Log($"Puzzle completato: {id}");
            PuzzleCompleted?.Invoke(id);
        }
    }

    public void SavePlayerState(Vector3 pos, Quaternion rot)
    {
        playerSavedPosition = pos;
        playerSavedRotation = rot;
        hasSavedPosition = true;
    }

    // >>> AGGIUNGI QUESTO <<<
    public bool TryGetSavedPlayerState(out Vector3 pos, out Quaternion rot)
    {
        if (hasSavedPosition)
        {
            pos = playerSavedPosition;
            rot = playerSavedRotation;
            return true;
        }
        pos = default;
        rot = default;
        return false;
    }

    // >>> E QUESTO <<<
    public void ClearSavedPlayerState()
    {
        hasSavedPosition = false;
    }
}