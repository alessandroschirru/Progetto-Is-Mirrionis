using UnityEngine;

public class PuzzleLightIndicator : MonoBehaviour
{
    private Light lightComp;
    private Renderer rend;

    public Color incompleteColor = Color.red;
    public Color completeColor = Color.green;

    public string puzzleID; // associa questo puzzleID al puzzle

    void Start()
    {
        lightComp = GetComponent<Light>();
        rend = GetComponent<Renderer>();

        UpdateLight();
    }

    public void UpdateLight()
    {
        bool isCompleted = GameStateManager.Instance.IsPuzzleCompleted(puzzleID);

        if (lightComp != null)
            lightComp.color = isCompleted ? completeColor : incompleteColor;

        if (rend != null)
            rend.material.color = isCompleted ? completeColor : incompleteColor;
    }
}