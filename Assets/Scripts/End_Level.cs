using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_Level : MonoBehaviour
{
    public Transform spawnPosition;
    public Transform playerTransform;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine("End");
        }
    }

    IEnumerator End()
    {
        Debug.Log("End of level");
        yield return new WaitForSeconds(1);
        playerTransform.position = spawnPosition.position;
    }
}
