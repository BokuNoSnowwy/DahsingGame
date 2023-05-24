using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Swipe swipe;
    private Collider2D col;

    IEnumerator waitLevel()
    {
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Waiting player");
            yield return null;
        }
        Debug.Log("Player is here");
        swipe = GameManager.Instance.Swipe;
    }

    private void Start()
    {
        col = GetComponent<Collider2D>();

        StartCoroutine(waitLevel());
    }

    private void Update()
    {
        if (swipe.rb.velocity.y > 0)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }
}
