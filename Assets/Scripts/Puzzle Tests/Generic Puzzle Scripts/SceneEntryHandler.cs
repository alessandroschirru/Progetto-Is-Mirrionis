using UnityEngine;

public class SceneEntryHandler : MonoBehaviour
{
    void Start()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.hasSavedPosition)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = GameStateManager.Instance.playerSavedPosition;
                player.transform.rotation = GameStateManager.Instance.playerSavedRotation;
            }
        }
    }
}