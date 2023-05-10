using UnityEngine;

public class Reset : Interactive
{
    public override void DetectPlayer()
    {
        playerSwipe.hasDashed = false;
    }
}
