using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMovement : Movement
{
    private Vector2 minPower = Vector2.one * -2;
    private Vector2 maxPower = Vector2.one * 2;

    private Vector3 startPoint;
    private Vector3 endPoint;

    private Vector2 forceArrow;
    private bool isPreparingDash;
    
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
        if (Input.GetButtonDown("Fire1") && !isPreparingDash)
        {
            //TODO Faire apparaitre la fleche style billard
            // Slow motion du jeu

            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isPreparingDash = true;

            Time.timeScale = 0.2f;
        }

        if (Input.GetButtonUp("Fire1") && isPreparingDash)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            forceArrow = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            
            Dash(forceArrow.x,forceArrow.y);
            
            isPreparingDash = false;
            Time.timeScale = 1;
        }
    }
}
