using UnityEngine;

public class LightsPuzzleManager : MonoBehaviour
{
    [Header("Tutte le luci del puzzle")]
    [Tooltip("Assegna qui le 6 luci, oppure attiva AutoFind e indica un root.")]
    public Light[] allLights = new Light[6];

    [Header("Auto-scoperta (opzionale)")]
    public bool autoFindLightsUnderRoot = false;
    public Transform lightsRoot; // se autoFind è true, prende tutte le Light sotto questo transform

    [Header("Oggetto da abilitare quando tutte sono verdi")]
    public GameObject objectToEnable;

    [Tooltip("Se true, una volta abilitato rimane attivo anche se le luci tornano rosse.")]
    public bool oneShot = true;

    [Header("Debug")]
    public bool logDetails = true;
    [Range(0f, 0.25f)] public float colorTolerance = 0.03f;

    bool alreadyCompleted = false;

    void Awake()
    {
        if ((allLights == null || allLights.Length == 0) && autoFindLightsUnderRoot && lightsRoot != null)
        {
            allLights = lightsRoot.GetComponentsInChildren<Light>(true);
            if (logDetails) Debug.Log($"[LightsPuzzleManager] Auto trovate {allLights.Length} luci sotto {lightsRoot.name}.");
        }
    }

    void Start()
    {
        // Assicurati che l'oggetto sia spento a inizio (se vuoi il comportamento oneShot)
        if (objectToEnable && oneShot) objectToEnable.SetActive(false);
        UpdateState("Start");
    }

    public void CheckAllOn()
    {
        UpdateState("Event");
    }

    void UpdateState(string reason)
    {
        if (objectToEnable == null)
        {
            Debug.LogWarning("[LightsPuzzleManager] objectToEnable non assegnato.");
            return;
        }
        if (allLights == null || allLights.Length == 0)
        {
            Debug.LogWarning("[LightsPuzzleManager] Nessuna luce assegnata.");
            return;
        }

        bool allGreen = true;

        for (int i = 0; i < allLights.Length; i++)
        {
            var l = allLights[i];
            if (l == null)
            {
                allGreen = false;
                if (logDetails) Debug.LogWarning($"[LightsPuzzleManager] Luce indice {i} è NULL.");
                continue;
            }

            if (!IsApproximatelyGreen(l.color))
            {
                allGreen = false;
                if (logDetails) Debug.Log($"[LightsPuzzleManager] NON verde: {l.name} col={l.color} (reason={reason})");
            }
        }

        if (oneShot)
        {
            if (!alreadyCompleted && allGreen)
            {
                objectToEnable.SetActive(true);
                alreadyCompleted = true;
                if (logDetails) Debug.Log("[LightsPuzzleManager] TUTTE VERDI  attivo objectToEnable (oneShot).");
            }
        }
        else
        {
            objectToEnable.SetActive(allGreen);
            if (logDetails) Debug.Log($"[LightsPuzzleManager] SetActive({allGreen}) (reason={reason}).");
        }
    }

    bool IsApproximatelyGreen(Color c)
    {
        // Confronto con tolleranza per evitare problemi lineare/gamma o piccole variazioni
        return Mathf.Abs(c.r - Color.green.r) <= colorTolerance
            && Mathf.Abs(c.g - Color.green.g) <= colorTolerance
            && Mathf.Abs(c.b - Color.green.b) <= colorTolerance;
    }

    // Utility da Inspector per forzare il check
    [ContextMenu("Force Check Now")]
    void ForceCheckNow() => UpdateState("ContextMenu");
}