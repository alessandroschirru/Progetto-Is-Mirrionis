using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPuzzleScene : MonoBehaviour
{
    public GameObject coinsPuzzle;
    public GameObject photoPuzzle;
    public GameObject tube1Puzzle;
    public GameObject tube2Puzzle;
    public GameObject tube3Puzzle;
    public GameObject tube4Puzzle;

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
            SceneManager.LoadScene("LaboratoryScene");

            SceneManager.sceneLoaded += (scene, mode) =>
            {
                PuzzleLightManager.RefreshAllPuzzleLights(); // Metodo statico da chiamare se lo implementi
            };
            if (coinsPuzzle)
            {
                coinsPuzzle.SetActive(false);
            }            

            if (photoPuzzle)
            {
                photoPuzzle.SetActive(false);
            }            

            if (tube1Puzzle)
            {
                tube1Puzzle.SetActive(false);
            }

            if (tube2Puzzle)
            {
                tube2Puzzle.SetActive(false);
            }

            if (tube3Puzzle)
            {
                tube3Puzzle.SetActive(false);
            }

            if (tube4Puzzle)
            {
                tube4Puzzle.SetActive(false);
            }
        }
    }
}
