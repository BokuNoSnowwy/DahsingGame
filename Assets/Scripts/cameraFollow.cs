using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Vector3 newPos;
    private RectTransform rect;
    private Camera cam;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        cam = gameObject.GetComponent<Camera>();
        newPos = rect.position;
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
            Debug.Log("T mort");
        }
    }
}
