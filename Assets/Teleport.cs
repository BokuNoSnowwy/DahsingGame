using UnityEngine;

public class Teleport : Interactive
{
    [SerializeField]
    private GameObject entry, exit;

    public override void DetectPlayer()
    {
        playerSwipe.hasDashed = false;
        playerSwipe.gameObject.transform.position = exit.transform.position;
    }
}
