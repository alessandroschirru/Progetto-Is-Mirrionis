using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public SnapPoint targetPoint;  // a quale SnapPoint si deve connettere
    [HideInInspector] public PuzzlePiece parentPiece; // il pezzo a cui appartiene (da settare automaticamente)
    public bool isSnapped = false;

    private void Awake()
    {
        if (parentPiece == null)
        {
            parentPiece = GetComponentInParent<PuzzlePiece>();
        }
    }

    public bool CanSnap()
    {
        return !isSnapped && targetPoint != null && !targetPoint.isSnapped;
    }
}