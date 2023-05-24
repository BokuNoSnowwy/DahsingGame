using System.Collections;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Swipe playerMove;
    private Vector2 respawnPos;

    private Vector3 newPos;
    private RectTransform rect;
    private Camera cam;

    IEnumerator waitLevel()
    {
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Waiting player");
            yield return null;
        }
        Debug.Log("Player is here");
        player = GameManager.Instance.Player.gameObject;
    }

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        cam = gameObject.GetComponent<Camera>();
        newPos = rect.position;
        StartCoroutine(waitLevel());
    }

    void Update()
    {
        if (player.transform.position.y >= rect.position.y)
        {
            newPos.y = player.transform.position.y;
            rect.position = newPos;
        }
        if (player.transform.position.y <= rect.position.y - cam.orthographicSize)
        {
            respawnPos = player.transform.position;
            playerMove.rb.velocity = Vector2.zero;
            playerMove.noGravity = true;
            respawnPos.y = rect.position.y - cam.orthographicSize + 1;
            player.transform.position = respawnPos;
            playerMove.hasDashed = false;
        }
    }
}
