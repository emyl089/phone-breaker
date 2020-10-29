using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    private int Score = 0;
    private int Health = 100;
    public bool Died;

    public Transform spawnPosition;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        Died = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Died == true)
        {
            StartCoroutine ("Die");
            
        }

        if(Health < 0)
        {
            Died = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Died = true;
        }
    }

    IEnumerator Die()
    {
        Debug.Log("Player has fallen");
        yield return new WaitForSeconds(1);
        playerTransform.position = spawnPosition.position;
        Died = false;
    }
}
