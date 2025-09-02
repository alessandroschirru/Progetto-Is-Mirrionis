using UnityEngine;

public class PuzzleLightManager : MonoBehaviour
{
    void Start()
    {
        RefreshAllPuzzleLights(); // chiama l'aggiornamento al caricamento
    }

    public static void RefreshAllPuzzleLights()
    {
        var lights = FindObjectsByType<PuzzleLightIndicator>(FindObjectsSortMode.None);

        foreach (var light in lights)
        {
            light.UpdateLight();
        }
    }
}