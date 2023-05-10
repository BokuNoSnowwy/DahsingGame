using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    //Launch when detect player collision
    void DetectPlayer();
    //Launch on level reset
    void Reset();
    //Listen to various GameManager event
    void ListenEventGameManager();
}
