using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
	public Text CheckpointText;
	private Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCharacter playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            playerCharacter.SetCurrentCheckpoint(this);
            anim.SetBool("IsActive", true);
			CheckpointText.text = "Checkpoint opened!";
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
		CheckpointText.text = "";
    }
}
