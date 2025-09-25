using System;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = transform.position;
        }
    }
}


