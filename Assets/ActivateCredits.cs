using UnityEngine;

public class ActivateCredits : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject credits;
    public GameObject hud;

    private void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD");
    }
    public void SetActiveCredits ()
    {
        //hud.gameObject.SetActive(false);
        credits.SetActive(true);
        Time.timeScale = 0f;        
    }
     
}
