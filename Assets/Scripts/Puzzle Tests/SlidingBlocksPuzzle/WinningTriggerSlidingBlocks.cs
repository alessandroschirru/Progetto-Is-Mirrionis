using UnityEngine;

public class WinningTriggerSlidingBlocks : MonoBehaviour
{

    public GameObject winningText;
    public GameObject mainBlock;
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
            GameStateManager.Instance.MarkPuzzleAsCompleted("PuzzleSlidingBlocks");
        }
    }
}
