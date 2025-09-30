using UnityEngine;

[RequireComponent(typeof(LeverSwitch))]
public class LeverLights : MonoBehaviour
{
    [Tooltip("Le 2 luci controllate da questa leva")]
    public Light[] lightsToControl = new Light[2];

    [Tooltip("Manager globale che verifica se tutte le luci sono ON")]
    public LightsPuzzleManager puzzleManager;

    LeverSwitch lever;

    void Awake()
    {
        lever = GetComponent<LeverSwitch>();
        if (puzzleManager == null) puzzleManager = FindFirstObjectByType<LightsPuzzleManager>();

        // quando la leva va ON/OFF, aggiorna le luci
        lever.OnTurnOn.AddListener(() => SetLights(true));
        lever.OnTurnOff.AddListener(() => SetLights(false));
    }

    void Start()
    {
        // Stato iniziale coerente con la leva
        SetLights(lever.isOn);
    }

    void SetLights(bool on)
    {
        foreach (var l in lightsToControl)
        {
            if (!l) continue;
            l.enabled = true;                 // manteniamo accese per vedere il colore
            l.color = on ? Color.green : Color.red;
        }

        if (puzzleManager) puzzleManager.CheckAllOn();
    }
}