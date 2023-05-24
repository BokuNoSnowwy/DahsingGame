using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMoving : Trap
{
    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;

    [SerializeField] private float moveSpeed;

    private Vector2 dest;
    private Vector2 pos1Vector;
    private Vector2 pos2Vector;

    private bool isMoving = true;
    private bool atPos1 = true;
    private bool atPos2 = false;


    // Start is called before the first frame update
    void Start()
    {
        pos1Vector = pos1.transform.position;
        pos2Vector = pos2.transform.position;

        dest = pos2Vector;

        StartCoroutine(Moving());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Moving()
    {
        while (isMoving)
        {
            if (Vector2.Distance(transform.position, dest) <= 0)
            {
                if (atPos1 == true)
                {
                    atPos1 = false;
                    atPos2 = true;
                    dest = pos2Vector;
                }
                else
                {
                    atPos1 = true;
                    atPos2 = false;
                    dest = pos1Vector;
                }

            }
            transform.position = Vector2.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);
            yield return null;
        }

        
    }
}
