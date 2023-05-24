using System.Collections;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private GameObject player;
    private Vector2 respawnPos;

    private Vector3 newPos;
    [HideInInspector]
    public RectTransform rect;
    [HideInInspector]
    public Vector3 firstPos;
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
        firstPos = rect.position;
        StartCoroutine(waitLevel());
    }

    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.y >= rect.position.y)
            {
                newPos.y = player.transform.position.y;
                rect.position = newPos;
            }
        }
    }
}
