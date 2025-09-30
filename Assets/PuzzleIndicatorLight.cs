using UnityEngine;

public class PuzzleIndicatorLight : MonoBehaviour
{
    [Header("Puzzle")]
    public string puzzleID;

    [Header("Output visivo")]
    public Light targetLight;                 // opzionale: la vera lampada
    public Renderer[] renderersToTint;        // opzionale: mesh/material della lampadina

    [Header("Colori")]
    public Color incompleteColor = Color.red;
    public Color completeColor = Color.green;

    [Header("Shader props (se usi materiale)")]
    public string baseColorProp = "_Color";         // URP Lit spesso è _BaseColor
    public string emissionColorProp = "_EmissionColor";
    public bool useEmission = true;
    public float emissionMultiplier = 2f;

    void Start()
    {
        // Stato iniziale
        bool done = GameStateManager.Instance != null &&
                    GameStateManager.Instance.IsPuzzleCompleted(puzzleID);
        ApplyVisual(done);

        // Sottoscrizione evento (se mai servirà aggiornare “live”)
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
        if (id == puzzleID)
            ApplyVisual(true);
    }

    void ApplyVisual(bool completed)
    {
        var col = completed ? completeColor : incompleteColor;

        if (targetLight != null)
        {
            targetLight.color = col;
            targetLight.enabled = true;
        }

        if (renderersToTint != null)
        {
            foreach (var r in renderersToTint)
            {
                if (!r) continue;
                var mat = r.material; // istanzia materiale per questo renderer
                if (mat.HasProperty(baseColorProp))
                    mat.SetColor(baseColorProp, col);

                if (useEmission && mat.HasProperty(emissionColorProp))
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor(emissionColorProp, col * emissionMultiplier);
                }
            }
        }
    }
}