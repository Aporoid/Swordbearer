using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool isPlayerInTrigger;

    private int levelNumber = 0;

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
            if(levelNumber == 0)
            {
                Debug.Log("Check it out");
                SceneManager.LoadScene("Cave2");
                levelNumber++;
            }
            else if (levelNumber == 1)
            {
                SceneManager.LoadScene("Cemetary");
            }
        }
    }
}
