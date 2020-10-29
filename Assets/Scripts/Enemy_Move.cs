using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    private int enemySpeed = 5;
    private int xMoveDirection = 1;
    private bool facingRight = true;

    public Transform spawnPosition;
    public Transform playerTransform;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
        if (hit.distance < 0.8f)
        {
            if (hit.collider.tag == "Player")
            {
                StartCoroutine("Die");
            }
            else return;
        }
        else return;
    }

    private void OnCollisionEnter2D(Collision2D target)

    {
        if (target.gameObject.tag.Equals("Object") == true)
        {
            xMoveDirection *= -1;
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    IEnumerator Die()
    {
        Debug.Log("Player died");
        yield return new WaitForSeconds(0);
        playerTransform.position = spawnPosition.position;
    }
}
