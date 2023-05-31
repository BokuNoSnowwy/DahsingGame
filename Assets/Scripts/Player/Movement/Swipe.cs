using UnityEngine;

public class Swipe : Movement
{
    //Swipe
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    [SerializeField]
    private float forceDash = 500f;
    private Vector2 direction;

    new void Start()
    {
        base.Start();

        Dash(0,0);
    }

    new void Update()
    {
        base.Update();

        if (!hasDashed)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;

                direction = forceDash * (endTouchPosition - startTouchPosition).normalized;
                
                Dash(direction.x, direction.y);

                AudioManager.instance.Play("Dash");
            }
        }

        if (noGravity)
        {
            rb.gravityScale = 0;
        }

        Debug.Log("dashSpeed : " + dashSpeed + " / speed : " + speed + " / velocity : " + rb.velocity.magnitude);
        Debug.Log("forceDash : " + forceDash);
    }
}
