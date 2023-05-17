using UnityEngine;

public interface IInteractable
{
    Vector2 iniPos { get; set; }
    //Launch when detect player collision
    void DetectPlayer(Movement playerMovement);
    //Launch on level reset
    void ResetInteractable();
    //Listen to various GameManager event
    void ListenEventGameManager();
}
