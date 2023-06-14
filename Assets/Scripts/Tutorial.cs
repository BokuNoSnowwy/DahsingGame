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
}
