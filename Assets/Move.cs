using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float playerSpeed = 10;
    public float moveX;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        if (moveX > 0.0f)
        {
            playerSpeed = moveX - 9;
        }
        else if (moveX < 0.0f)
        {
            playerSpeed = moveX + 9;
        }
        else
            playerSpeed = 0;
        rb.AddTorque(playerSpeed, ForceMode2D.Force);
    }
}
