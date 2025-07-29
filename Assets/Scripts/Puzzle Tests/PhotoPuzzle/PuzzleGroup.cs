using System.Collections.Generic;
using UnityEngine;

public class PuzzleGroup
{
    public List<PuzzlePiece> pieces = new List<PuzzlePiece>();

    public void AddPiece(PuzzlePiece piece)
    {
        if (!pieces.Contains(piece))
        {
            pieces.Add(piece);
            piece.group = this;
        }
    }

    public void Merge(PuzzleGroup otherGroup)
    {
        foreach (var piece in otherGroup.pieces)
        {
            AddPiece(piece); // aggiunge anche al nuovo gruppo
        }
    }

    public void MoveGroup(Vector3 delta)
    {
        foreach (var piece in pieces)
        {
            piece.transform.position += delta;
        }
    }
}