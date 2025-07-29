using UnityEngine;
using System.Collections.Generic;

public class PhotoPuzzleManager : MonoBehaviour
{
    public GameObject winText;
    private List<PuzzlePiece> allPieces = new List<PuzzlePiece>();
    private bool hasWon = false;

    void Start()
    {
        allPieces.AddRange(Object.FindObjectsByType<PuzzlePiece>(FindObjectsSortMode.None));
        winText.SetActive(false);
    }

    void Update()
    {
        if (!hasWon && IsPuzzleComplete())
        {
            hasWon = true;
            winText.SetActive(true);
            GameStateManager.Instance.MarkPuzzleAsCompleted("PuzzlePhoto");
        }
    }

    bool IsPuzzleComplete()
    {
        if (allPieces.Count == 0) return false;

        PuzzleGroup group = allPieces[0].group;

        foreach (var piece in allPieces)
        {
            if (piece.group != group)
                return false;
        }

        return true;
    }
}