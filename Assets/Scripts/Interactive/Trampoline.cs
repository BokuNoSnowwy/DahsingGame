using UnityEngine;

public class Trampoline : Interactive
{
    [SerializeField]
    private float power = 50;

    public override void DetectPlayer(Movement playerMovement)
    {
        if (playerSwipe.rb.velocity.y < 0)
        {
            AudioManager.instance.Play("Trampoline");
            playerSwipe.hasDashed = false;
            playerSwipe.rb.AddForce(transform.up * power, ForceMode2D.Impulse);
        }  
    }
}
