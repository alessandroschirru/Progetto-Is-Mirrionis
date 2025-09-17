using UnityEngine;

public class WinningTriggerSlidingBlocks : MonoBehaviour
{

    public GameObject winningText;
    public GameObject mainBlock;
    public int tubePuzzleNumber;
    private BoxCollider2D boxCollider;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (mainBlock.transform.position == transform.position)
        {
            winningText.SetActive(true);
            boxCollider.enabled = true;

            switch (tubePuzzleNumber)
            {
                case 1:
                    GameStateManager.Instance.MarkPuzzleAsCompleted("PuzzleTubes1");
                    break;

                case 2:
                    GameStateManager.Instance.MarkPuzzleAsCompleted("PuzzleTubes2");
                    break;

                case 3:
                    GameStateManager.Instance.MarkPuzzleAsCompleted("PuzzleTubes3");
                    break;

                case 4:
                    GameStateManager.Instance.MarkPuzzleAsCompleted("PuzzleTubes4");
                    break;
            }

        }
    }
}
