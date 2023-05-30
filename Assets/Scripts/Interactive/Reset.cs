
using UnityEngine;

public class Reset : Interactive
{
    public bool actif;
    [HideInInspector]
    public Color spriteColor;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void DetectPlayer(Movement playerMovement)
    {
        if (actif)
        {
            playerMovement.hasDashed = false;
            AudioManager.instance.Play("Zap");
            devientInactif();
        }
    }

    public void devientActif()
    {
        spriteColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        spriteRenderer.color = spriteColor;
        actif = true;
    }
    public void devientInactif()
    {
        spriteColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
        spriteRenderer.color = spriteColor;
        actif = false;
    }

}
