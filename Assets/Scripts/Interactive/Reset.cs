
public class Reset : Interactive
{
    public override void DetectPlayer(Movement playerMovement)
    {
        playerSwipe.hasDashed = false;
    }
}
