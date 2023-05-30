using UnityEngine;

public class ResetComplex : Interactive
{
    [SerializeField]
    private Reset[] L_reset;
    private Reset resetActif;
    private int nReset;
    private float timerMax = 3f;
    private float timer;

    private void Start()
    {
        nReset = 0;
        resetActif = L_reset[nReset];
        resetActif.actif = true;
        timer = timerMax;
    }

    private void Update()
    {
        if (!resetActif.actif && nReset < L_reset.Length - 1)
        {
            nReset++;
            resetActif = L_reset[nReset];
            resetActif.devientActif();
            timer = timerMax;
        }

        if (nReset > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ResetInteractable();
            }
        }
    }

    public override void ResetInteractable()
    {
        nReset = 0;
        timer = timerMax;
        foreach (Reset reset in L_reset)
        {
            reset.devientInactif();
        }
        L_reset[0].devientActif();
        resetActif = L_reset[0];
    }
}
