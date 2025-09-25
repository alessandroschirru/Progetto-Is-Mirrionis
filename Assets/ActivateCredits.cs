using UnityEngine;

public class ActivateCredits : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject credits;

    public void SetActiveCredits ()
    {
        credits.SetActive(true);
        Time.timeScale = 0f;
    }
     
}
