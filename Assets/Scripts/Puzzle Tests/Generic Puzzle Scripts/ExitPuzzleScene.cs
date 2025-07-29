using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPuzzleScene : MonoBehaviour
{
    public GameObject coinsPuzzle;
    public GameObject photoPuzzle;
    public GameObject slidingBlocksPuzzle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("ScenaTestAlessandro");

            if (coinsPuzzle)
            {
                coinsPuzzle.SetActive(false);
            }            

            if (photoPuzzle)
            {
                photoPuzzle.SetActive(false);
            }            

            if (slidingBlocksPuzzle)
            {
                slidingBlocksPuzzle.SetActive(false);
            }         
        }
    }
}
