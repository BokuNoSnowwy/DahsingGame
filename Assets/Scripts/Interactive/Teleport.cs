using UnityEngine;

public class Teleport : Interactive
{
    [SerializeField]
    private GameObject entry, exit;
    public bool keepPlayer;

    public override void DetectPlayer()
    {
        playerSwipe.hasDashed = false;
        playerSwipe.gameObject.transform.position = exit.transform.position;
        if (keepPlayer)
        {
            playerSwipe.rb.velocity = Vector2.zero;
            playerSwipe.noGravity = true;
        }
    }
}
