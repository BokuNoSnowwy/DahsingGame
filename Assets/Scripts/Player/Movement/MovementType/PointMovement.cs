using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMovement : Movement
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void DashMovement()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            touchPos = Camera.main.ScreenToWorldPoint(touchPos);
            touchPos = new Vector3(touchPos.x, touchPos.y, 0);

            Vector3 heading = (transform.position - touchPos).normalized;
            Vector3 direction = heading / heading.magnitude;

            float xRaw = -direction.x;
            float yRaw = -direction.y;

            if (touch.phase == TouchPhase.Ended)
            {
                if (xRaw != 0 || yRaw != 0)
                    Dash(xRaw, yRaw);
            }
        }
    }
}
