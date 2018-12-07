using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            PlayerCharacter playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            playerCharacter.SetCurrentCheckpoint(this);
            anim.SetBool("IsActive", true);
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}
