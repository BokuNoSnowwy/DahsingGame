using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HidingPlatform : MonoBehaviour
{
    private Collider2D collider;
    private Tilemap tilemap;
    private TilemapRenderer tilemapRend;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddListenerSceneIsLoaded(Initialization);

        collider = GetComponent<Collider2D>();
        tilemap = GetComponent<Tilemap>();
        tilemapRend = GetComponent<TilemapRenderer>();
    }

    private void Initialization()
    {
        GameManager.Instance.AddListenerPlayerRespawn(Reset);
        GameManager.Instance.Player.AddListenerFirstDashRespawn(HidePlatform);
    }

    private void Reset()
    {
        collider.enabled = true;
        tilemapRend.enabled = true;
        tilemap.enabled = true;
    }

    private void HidePlatform()
    {
        collider.enabled = false;
        tilemapRend.enabled = false;
        tilemap.enabled = false;
    }
}
