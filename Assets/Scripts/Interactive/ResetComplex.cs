using UnityEngine;

public class ResetComplex : Interactive
{
    [SerializeField]
    private GameObject[] L_reset;
    private GameObject resetActif;
    private int nReset;

    private void Start()
    {
        nReset = 0;
        resetActif = L_reset[nReset];
    }

    public override void DetectPlayer()
    {
        resetActif.SetActive(false);
        if (nReset < L_reset.Length - 1)
        {
            nReset = nReset + 1;
            resetActif = L_reset[nReset];
            resetActif.SetActive(true);
        }
        playerSwipe.hasDashed = false;
    }
}
