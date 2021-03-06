﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            playerCharacter.KillPlayer();
        } 
    }
}
