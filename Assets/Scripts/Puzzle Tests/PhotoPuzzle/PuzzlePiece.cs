using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [HideInInspector] public PuzzlePiece parentPiece; // il pezzo a cui appartiene (da settare automaticamente)
    public List<SnapPoint> snapPoints = new List<SnapPoint>();
    public PuzzleGroup group;

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        if (group == null)
        {
            group = new PuzzleGroup();
            group.AddPiece(this);
        }

        snapPoints.AddRange(GetComponentsInChildren<SnapPoint>());
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mouseDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
            mouseDelta.z = 0;
            group.MoveGroup(mouseDelta);
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            TrySnapToNearbyPoints();
        }
    }

    public void OnMouseDown()
    {
        isDragging = true;
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }

    void TrySnapToNearbyPoints()
    {
        foreach (var mySnap in snapPoints)
        {
            if (!mySnap.CanSnap()) continue;

            var target = mySnap.targetPoint;
            Vector3 myPos = mySnap.transform.position;
            Vector3 targetPos = target.transform.position;

            float distance = Vector3.Distance(myPos, targetPos);
            if (distance < 0.08f)
            {
                // Calcola correzione
                Vector3 correction = targetPos - myPos;
                group.MoveGroup(correction);

                mySnap.isSnapped = true;
                target.isSnapped = true;

                // Unisci i gruppi
                if (group != target.parentPiece.group)
                {
                    group.Merge(target.parentPiece.group);
                }

                break; // Snappa solo una volta per frame
            }
        }
    }
}