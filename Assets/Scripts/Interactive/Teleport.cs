using UnityEngine;

public class Teleport : Interactive
{
    [SerializeField]
    private GameObject entry, exit;
    public bool keepPlayer;
    
    [Header("Points Display")]
    public int numberOfPoints; // Nombre de points à instancier
    public GameObject pointPrefab; // Préfabriqué du point à instancier

    protected override void Start()
    {
        base.Start();
        InstanciatePoints();
    }

    public override void DetectPlayer(Movement playerMovement)
    {
        playerMovement.EndDash();
        playerMovement.hasDashed = false;
        playerMovement.gameObject.transform.position = exit.transform.position;
        AudioManager.instance.Play("Teleport");
        playerMovement.rb.velocity = Vector2.zero;
        if (keepPlayer)
        {
            playerMovement.noGravity = true;
            playerMovement.rb.isKinematic = true;
        }
    }

    private void InstanciatePoints()
    {
        // Calcul de la distance totale entre les deux points
        float totalDistance = Vector3.Distance(entry.transform.position, exit.transform.position);

        // Calcul de la distance entre chaque point
        float distanceBetweenPoints = totalDistance / (numberOfPoints + 1);

        // Calcul du vecteur direction entre les deux points
        Vector3 direction = (exit.transform.position - entry.transform.position).normalized;

        for (int i = 1; i <= numberOfPoints; i++)
        {
            // Calcul de la position du point à instancier
            Vector3 pointPosition = entry.transform.position + (direction * (distanceBetweenPoints * i));

            // Instanciation du point
            Instantiate(pointPrefab, pointPosition, Quaternion.identity,transform);
        }
    }
}
