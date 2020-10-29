using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [Range(0,6)]
	public float playerSpeed;
	private bool facingRight = false;
    private float moveX;
    private float distToPlayerBottom = 0.60f;

    private int playerJumpPower = 12;
    public bool isGrounded;

    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void Update()
    {
        Jump();
        PlayerRaycast();
    }

    void PlayerMove()
	{
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if (moveX != 0f)
        {
            playerSpeed += Time.deltaTime;
        }
        else
            playerSpeed = 0;
        playerSpeed = Mathf.SmoothStep(6, -6, Time.deltaTime / 10);

        Vector2 move = new Vector2(moveX, 0);
        rb.AddForce(move * playerSpeed * Time.deltaTime);

        //ANIMATIONS

        //PLAYER DIRECTION
        if (moveX < 0.0f && facingRight == false)
		{
			FlipPlayer();
		}
		else if (moveX > 0.0f && facingRight == true)
		{
			FlipPlayer();
		}

        //PHYSICS
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
	}

    //JUMP
    void Jump()
    {
        if (isGrounded == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * playerJumpPower;
        }
        if (Input.GetKey(KeyCode.UpArrow) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * playerJumpPower;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }

    }

    //Player faceing
    void FlipPlayer()
	{
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
	}

    void PlayerRaycast()
    {
        //Ray Up
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUp != null && rayUp.collider != null && rayUp.distance < distToPlayerBottom && rayUp.collider.tag == "Enemy")
        {

        }

        //Ray Down
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if(rayDown != null && rayDown.collider != null && rayDown.distance < distToPlayerBottom && rayDown.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
            rayDown.collider.gameObject.transform.localScale = new Vector3(1f, 0.2f, 1f);
            rayDown.collider.gameObject.GetComponent<Enemy_Move>().enabled = false;
            StartCoroutine("Kill");
        }

        if (rayDown != null && rayDown.collider != null && rayDown.distance < distToPlayerBottom && rayDown.collider.tag != "Enemy")
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }
    IEnumerator Kill()
    {
        Debug.Log("Enemy dead");
       
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if (rayDown != null && rayDown.collider != null && rayDown.distance < distToPlayerBottom && rayDown.collider.tag == "Enemy")
        {
            rayDown.collider.gameObject.layer = 2;
            yield return new WaitForSeconds(1);
            Destroy(rayDown.collider.gameObject);
        }
    }
}
