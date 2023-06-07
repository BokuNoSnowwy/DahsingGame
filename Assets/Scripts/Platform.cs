using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Movement playerMovement;
    private Collider2D col;

    IEnumerator waitLevel()
    {
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Waiting player");
            yield return null;
        }
        Debug.Log("Player is here");
        playerMovement = GameManager.Instance.playerMovement;
    }

    private void Start()
    {
        col = GetComponent<Collider2D>();

        StartCoroutine(waitLevel());
    }

    private void Update()
    {
        if (playerMovement != null && /*playerMovement.rb.velocity.y > 0*/ playerMovement.transform.position.y - .5f < transform.position.y)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }
}
