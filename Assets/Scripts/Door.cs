﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool isPlayerInTrigger;

    [SerializeField]
    private string levelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Activate") && isPlayerInTrigger)
        {
            if(levelName == "Cave2")
            {
                SceneManager.LoadScene("Cave2");
            }
            else if (levelName == "Cemetary")
            {
                SceneManager.LoadScene("Cemetary");
            }
			else if (levelName == "EndSlate")
			{
				SceneManager.LoadScene("EndScene");
			}
        }
    }
}
