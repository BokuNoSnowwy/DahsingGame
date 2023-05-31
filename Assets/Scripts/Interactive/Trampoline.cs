using UnityEngine;

public class Trampoline : Interactive
{
    [SerializeField]
    private float power = 50;
    private Animation anim;


    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }


    public override void DetectPlayer(Movement playerMovement)
    {
        if (playerMovement.rb.velocity.y < 0)
        {
            AudioManager.instance.Play("Trampoline");
            playerMovement.hasDashed = false;
            playerMovement.rb.velocity = Vector2.zero;
            playerMovement.rb.AddForce(transform.up * power, ForceMode2D.Impulse);
            anim.Play("Jumppad");
        }
    }
}
