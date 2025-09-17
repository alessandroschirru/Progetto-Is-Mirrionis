using UnityEngine;

public class ConnectionChecker : MonoBehaviour
{
    int colLayer;

    void Awake()
    {
        colLayer = LayerMask.NameToLayer("ColTubi");
        if (gameObject.layer != colLayer)
            Debug.LogWarning($"{name}: metti il layer 'ColTubi' su questo collider trigger.");
    }

    GameObject TubeRoot(Transform t)
    {
        var p = t;
        while (p != null && p.GetComponent<PipePiece>() == null) p = p.parent;
        return p ? p.gameObject : t.gameObject; // fallback
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != colLayer) return;

        var a = TubeRoot(transform);
        var b = TubeRoot(other.transform);
        if (a == b) return;

        // dedup
        if (a.GetInstanceID() < b.GetInstanceID())
        {
            Debug.Log($"CONNECT {a.name} <-> {b.name}");
            TubesPath.Connect(a, b);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != colLayer) return;

        var a = TubeRoot(transform);
        var b = TubeRoot(other.transform);
        if (a == b) return;

        if (a.GetInstanceID() < b.GetInstanceID())
        {
            Debug.Log($"DISCONNECT {a.name} <-> {b.name}");
            TubesPath.Disconnect(a, b);
        }
    }

    void OnDisable()
    {
        var a = TubeRoot(transform);
        TubesPath.RemoveNode(a);
    }
}