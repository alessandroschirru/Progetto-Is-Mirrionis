using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public Vector3 playerSavedPosition;
    public Quaternion playerSavedRotation;
    public bool hasSavedPosition = false;

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

    public void SavePlayerState(Vector3 pos, Quaternion rot)
    {
        playerSavedPosition = pos;
        playerSavedRotation = rot;
        hasSavedPosition = true;
    }

    public void MarkPuzzleAsCompleted(string puzzleID)
    {
        completedPuzzles.Add(puzzleID);
    }

    public bool IsPuzzleCompleted(string puzzleID)
    {
        return completedPuzzles.Contains(puzzleID);
    }
}