using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TubesPath : MonoBehaviour
{
    public static TubesPath Instance { get; private set; }

    [Header("Capo e coda del percorso")]
    public GameObject firstTube;   // sorgente
    public GameObject finalTube;   // destinazione

    // Grafo: per ogni tubo, l'insieme dei vicini collegati
    private static readonly Dictionary<GameObject, HashSet<GameObject>> graph = new();

    // Path corrente (ricostruito ad ogni update)
    public static readonly List<GameObject> currentPath = new();

    void Awake()
    {
        Instance = this;
        graph.Clear();
        currentPath.Clear();
    }

    // --- API chiamate dai collider ---
    public static void Connect(GameObject a, GameObject b)
    {
        if (a == null || b == null || a == b) return;
        if (!graph.ContainsKey(a)) graph[a] = new HashSet<GameObject>();
        if (!graph.ContainsKey(b)) graph[b] = new HashSet<GameObject>();
        if (graph[a].Add(b) | graph[b].Add(a))
            Debug.Log($"Graph + {a.name} <-> {b.name}");
        RebuildPathStatic();
    }

    public static void Disconnect(GameObject a, GameObject b)
    {
        if (a == null || b == null || a == b) return;
        bool changed = false;
        if (graph.TryGetValue(a, out var sa)) changed |= sa.Remove(b);
        if (graph.TryGetValue(b, out var sb)) changed |= sb.Remove(a);
        if (changed) Debug.Log($"Graph - {a.name} <-> {b.name}");
        RebuildPathStatic();
    }
    public static void RemoveNode(GameObject a)
    {
        if (a == null) return;
        if (graph.TryGetValue(a, out var neighs))
        {
            foreach (var n in neighs.ToList()) graph[n].Remove(a);
            graph.Remove(a);
        }
        RebuildPathStatic();
    }

    public static void RebuildPathStatic()
    {
        if (Instance != null) Instance.RebuildPath();
    }

    // --- BFS e ricostruzione del percorso ---
    private void RebuildPath()
    {
        currentPath.Clear();
        if (firstTube == null || finalTube == null) return;
        if (!graph.ContainsKey(firstTube))
        {
            Debug.Log("Ancora non collegato");
            return;
        }

        var prev = new Dictionary<GameObject, GameObject>();
        var visited = new HashSet<GameObject>();
        var q = new Queue<GameObject>();

        visited.Add(firstTube);
        q.Enqueue(firstTube);

        while (q.Count > 0)
        {
            var u = q.Dequeue();
            if (u == finalTube) break;

            if (!graph.TryGetValue(u, out var neighs)) continue;
            foreach (var v in neighs)
            {
                if (visited.Add(v))
                {
                    prev[v] = u;
                    q.Enqueue(v);
                }
            }
        }

        if (visited.Contains(finalTube))
        {
            // ricostruisci il path first -> final
            var stack = new List<GameObject>();
            var cur = finalTube;
            stack.Add(cur);
            while (cur != firstTube)
            {
                cur = prev[cur];
                stack.Add(cur);
            }
            stack.Reverse();
            currentPath.AddRange(stack);

            Debug.Log("PERCORSO COMPLETO: " +
                      string.Join(" -> ", currentPath.Select(go => go.name)));
            // TODO: qui puoi triggerare la fine del puzzle
        }
        else
        {
            Debug.Log("Ancora non collegato");
        }
    }
}