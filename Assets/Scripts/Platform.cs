using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private Swipe player;
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (player.rb.velocity.y > 0)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }
}
