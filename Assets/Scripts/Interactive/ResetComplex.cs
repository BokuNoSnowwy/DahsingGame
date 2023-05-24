using UnityEngine;

public class ResetComplex : Interactive
{
    [SerializeField]
    private Reset[] L_reset;
    private Reset resetActif;
    private int nReset;

    private void Start()
    {
        nReset = 0;
        resetActif = L_reset[nReset];
        resetActif.actif = true;
    }

    private void Update()
    {
        if (!resetActif.actif && nReset < L_reset.Length - 1)
        {
            nReset++;
            resetActif = L_reset[nReset];
            resetActif.devientActif();
        }
    }

    public override void ResetInteractable()
    {
        nReset = 0;
        foreach (Reset reset in L_reset)
        {
            reset.spriteColor = new Color(reset.spriteColor.r, reset.spriteColor.g, reset.spriteColor.b, 0.2f);
        }
        L_reset[0].spriteColor = new Color(L_reset[0].spriteColor.r, L_reset[0].spriteColor.g, L_reset[0].spriteColor.b, 1f);
    }
}
