using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        float playerInput = playerInput.GetAxis("Horizontal");
        rb.velocity = new Vector2(playerInput * playerSpeed, rb.velocity.y);
    }
}
