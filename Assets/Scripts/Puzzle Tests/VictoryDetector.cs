using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VictoryDetector : MonoBehaviour
{
    private bool victory = false;
    public GameObject coin1;
    public GameObject coin2;
    public GameObject coin3;
    public GameObject coin4;
    public GameObject coin5;
    public GameObject coinSpot1;
    public GameObject coinSpot2;
    public GameObject coinSpot3;
    public GameObject coinSpot4;
    public GameObject coinSpot5;
    public GameObject victoryText;

    void Start()
    {
        victoryText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!victory)
        {
            if(coin1.transform.position == coinSpot1.transform.position && coin2.transform.position == coinSpot2.transform.position && coin3.transform.position == coinSpot3.transform.position && coin4.transform.position == coinSpot4.transform.position && coin5.transform.position == coinSpot5.transform.position)
            {
                victoryText.gameObject.SetActive(true);
                victory = true;
            }
        }
    }
}
