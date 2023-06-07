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

    private TrajectoryLine tl;
    
    // Start is called before the first frame update
    void Start()
    {
        tl = GetComponentInChildren<TrajectoryLine>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        DashMovement();
    }

    public void DashMovement()
    {
        if (!hasDashed)
        {
            if (Input.GetButtonDown("Fire1") && !isPreparingDash)
            {
                //TODO Faire apparaitre la fleche style billard
                // Slow motion du jeu

                startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isPreparingDash = true;

                Time.timeScale = 0.2f;
            }

            if (Input.GetButton("Fire1") && isPreparingDash)
            {
                Vector3 currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 endPoint = transform.position + (currentPoint - startPoint);
                tl.RenderLine(transform.position, transform.position + (currentPoint - startPoint));


                float distance = Vector3.Distance(endPoint, transform.position);

                // Distance maximale autorisée
                float maxDistance = 1.5f; // Modifier cette valeur selon vos besoins

                // Vérifier si la distance dépasse la limite
                if (distance > maxDistance)
                {
                    // Réduire la distance pour atteindre la limite tout en conservant la direction
                    endPoint = transform.position + (endPoint - transform.position).normalized * maxDistance;
                }

                //tl.RenderLine(transform.position, transform.position + (currentPoint - startPoint));
                tl.RenderLine(transform.position, endPoint);
            }


            if (Input.GetButtonUp("Fire1") && isPreparingDash)
            {
                endPoint = transform.position + Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPoint;

                forceArrow = new Vector2(Mathf.Clamp(transform.position.x - endPoint.x, minPower.x, maxPower.x),
                    Mathf.Clamp(transform.position.y - endPoint.y, minPower.y, maxPower.y));

                Dash(forceArrow.x, forceArrow.y);

                isPreparingDash = false;
                Time.timeScale = 1;
                AudioManager.instance.Play("Dash");
                tl.EndLine();
            }
        }
    }
}
