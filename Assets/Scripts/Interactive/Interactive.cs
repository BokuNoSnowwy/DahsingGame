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
    }

    public virtual void DetectPlayer()
    {
    }

    public void ListenEventGameManager()
    {
        throw new System.NotImplementedException();
    }

    public void ResetInteractable()
    {
        throw new System.NotImplementedException();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DetectPlayer();
        }
    }
}
