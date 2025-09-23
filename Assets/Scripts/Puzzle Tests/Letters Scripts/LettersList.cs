using System.Collections.Generic;
using UnityEngine;

public class LettersList : MonoBehaviour
{
    public static LettersList instance;

    // Lista lettere e note
    public GameObject letteraAlFiglio;
    public GameObject notaDiarioGiorno1;
    public GameObject notaDiarioGiorno12;
    public GameObject notaDiarioGiorno27;
    public GameObject notaDiarioGiorno35;
    public GameObject notaDiarioGiorno42;
    public GameObject notaDiarioGiorno50;
    public GameObject notaDiarioGiorno61;
    public GameObject notaDiarioGiorno65;
    public GameObject notaDiarioGiorno70;
    public GameObject notaDiarioGiorno73;
    public GameObject notaDiarioGiorno75;
    public GameObject notaDiarioGiorno80;
    public GameObject noteSparseGiorno15;
    public GameObject noteSparseGiorno65;
    public GameObject noteSparseGiorno70;

    private bool letterOpen = true;
    private GameObject currentLetter;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 0f;
        currentLetter = letteraAlFiglio;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && letterOpen)
        {
            letterOpen = false;
            Time.timeScale = 1f;
            currentLetter.SetActive(false);
        }
    }

}




