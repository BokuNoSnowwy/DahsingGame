
public class Reset : Interactive
{
    public override void DetectPlayer(Movement playerMovement)
    {
        playerMovement.hasDashed = false;
        AudioManager.instance.Play("Zap");
    }
}
