using UnityEngine;

public interface IInteractable
{
    Vector2 iniPos { get;}
    //Launch when detect player collision
    void DetectPlayer();
    //Launch on level reset
    void ResetInteractable();
    //Listen to various GameManager event
    void ListenEventGameManager();
}
