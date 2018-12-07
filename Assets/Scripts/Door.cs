using System.Collections;
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
            if(levelName == "Scene2") // loads Cave 2 because I named that as Scene 2 originally, was too much work to rename
            {
                Debug.Log("Check it out");
                SceneManager.LoadScene("Cave2");
            }
            else if (levelName == "Cemetary")
            {
                SceneManager.LoadScene("Cemetary");
            }
        }
    }
}
