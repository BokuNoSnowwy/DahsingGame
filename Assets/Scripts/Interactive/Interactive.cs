using UnityEngine;

public class Interactive : MonoBehaviour, IInteractable
{
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

    protected virtual void Start()
    {
        GameManager.Instance.AddListenerPlayerRespawn(ResetInteractable);
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
