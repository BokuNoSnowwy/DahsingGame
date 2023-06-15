using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] instructions;
    [SerializeField] private Image[] circles;

    [SerializeField] private GameObject arrowLeft, arrowRight;

    int currentInstruction = 0;

    [SerializeField] private Color circleColorOn, circleColorOff;

    [Header("Swipe")]
    public float maxRangeSwipe = 250f;

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    bool canSwipe = true;

    private void Update()
    {
        DoSwipe();
    }

    public void Switch(bool isLeft)
    {
        arrowLeft.SetActive(true);
        arrowRight.SetActive(true);

        instructions[currentInstruction].SetActive(false);
        circles[currentInstruction].color = circleColorOff;

        currentInstruction = isLeft ? currentInstruction - 1 : currentInstruction + 1;

        instructions[currentInstruction].SetActive(true);
        circles[currentInstruction].color = circleColorOn;

        if(currentInstruction == 0)
        {
            arrowLeft.SetActive(false);
        }
        if(currentInstruction == instructions.Length - 1)
        {
            arrowRight.SetActive(false);
        }
    }

    public void Close()
    {
        GameManager.Instance.GetActualLevel().tutorialCompleted = true;
        gameObject.SetActive(false);
        GameManager.Instance.isInTuto = false;
    }

    void DoSwipe()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    CheckSwipe();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                CheckSwipe();
                canSwipe = true;
            }
        }
    }

    void CheckSwipe()
    {
        if (Mathf.Abs(fingerDown.x - fingerUp.x) > maxRangeSwipe && Mathf.Abs(fingerDown.x - fingerUp.x) > Mathf.Abs(fingerDown.y - fingerUp.y) && canSwipe)
        {
            if (fingerDown.x - fingerUp.x > 0)
            {
                if (currentInstruction > 0)
                {
                    Switch(true);
                    canSwipe = false;
                }

            }
            else if (fingerDown.x - fingerUp.x < 0)
            {
                if (currentInstruction < instructions.Length - 1)
                {
                    Switch(false);
                    canSwipe = false;
                }
            }
            fingerUp = fingerDown;
        }
    }
}
