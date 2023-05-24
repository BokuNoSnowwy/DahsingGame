using UnityEngine;

public class Teleport : Interactive
{
    [SerializeField]
    private GameObject entry, exit;
    public bool keepPlayer;

    public override void DetectPlayer(Movement playerMovement)
    {
        playerMovement.hasDashed = false;
        playerMovement.gameObject.transform.position = exit.transform.position;
        AudioManager.instance.Play("Teleport");
        if (keepPlayer)
        {
            playerMovement.rb.velocity = Vector2.zero;
            playerMovement.noGravity = true;
        }
    }
}
