using UnityEngine;

public class Interactive : MonoBehaviour, IInteractable
{
    public Swipe playerSwipe;

    public Vector2 iniPos 
    {
        get 
        {
        return transform.position;
        }
        set
        {
            ;
        }
    }

    public virtual void DetectPlayer(Movement playerMovement)
    {
    }

    public void ListenEventGameManager()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ResetInteractable()
    {
    }
}
