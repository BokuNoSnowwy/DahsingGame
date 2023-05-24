using UnityEngine;

public class Trampoline : Interactive
{
    [SerializeField]
    private float power = 50;

    public override void DetectPlayer(Movement playerMovement)
    {
        if (playerMovement.rb.velocity.y < 0)
        {
            AudioManager.instance.Play("Trampoline");
            playerMovement.hasDashed = false;
            playerMovement.rb.AddForce(transform.up * power, ForceMode2D.Impulse);
        }  
    }
}
