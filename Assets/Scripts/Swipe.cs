using UnityEngine;

public class Swipe : Movement
{
    //Swipe
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    new void Update()
    {
        base.Update();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            Dash(endTouchPosition.x - startTouchPosition.x, endTouchPosition.y - startTouchPosition.y);
        }
    }
}
