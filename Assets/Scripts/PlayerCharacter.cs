using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private string name = "Player 1";

    [SerializeField]
    private float jumpHeight = 5, speed = 3;

    private bool hasKey;

    private bool isOnGround;

    private Rigidbody2D rigidbody2DInstance;

    private float horizontalInput;

    // Use this for initialization
	void Start () {
        rigidbody2DInstance = GetComponent<Rigidbody2D>();

        rigidbody2DInstance.gravityScale = 5;
   	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Translate(0, -0.01f, 0);
        GetInput();
        Move();
	}

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        rigidbody2DInstance.velocity = new Vector2(horizontalInput, 0);
    }
}
