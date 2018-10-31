using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entered checkpoint");
            PlayerCharacter playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            playerCharacter.SetCurrentCheckpoint(this);
        }
    }
}
