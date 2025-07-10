using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PuzzleTrigger2D : MonoBehaviour
{
    [SerializeField] private GameObject puzzleCanvas;

    private void Start()
    {
        if (puzzleCanvas != null)
        {
            puzzleCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (puzzleCanvas != null)
            {
                puzzleCanvas.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void ClosePuzzle()
    {
        if (puzzleCanvas != null)
        {
            puzzleCanvas.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
