using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public bool dashAvailable => !movementScript.hasDashed;
    public SpriteRenderer spriteRenderer;
    
    public bool isAlive;

    [SerializeField] 
    private Movement movementScript;

    [HideInInspector] public UnityEvent firstDashRespawn;
    [HideInInspector] public UnityEvent playerDie;
    
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialization()
    {
        movementScript.dashEvent.AddListener(() =>
        {
            firstDashRespawn.Invoke();
        });

        isAlive = true;
    }

    private void Update()
    {
        Debug.Log(spriteRenderer.isVisible);
        if (!spriteRenderer.isVisible && isAlive)
        {
            Die();
        }
    }

    [ContextMenu("Player Die")]
    public void Die()
    {
        Debug.LogError("Die");
        isAlive = false;
        //Disable the player before respawning to make sure the player won't do unnecessary moves  
        gameObject.SetActive(false);
        playerDie.Invoke();
    }

    public void RespawnPlayer(Vector3 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        isAlive = true;
        movementScript.rb.velocity = Vector2.zero;
        movementScript.rb.gravityScale = movementScript.gravity;
    }

    public void AddListenerFirstDashRespawn(UnityAction action)
    {
        firstDashRespawn.AddListener(action);
    }
    
    public void AddListenerPlayerDie(UnityAction action)
    {
        playerDie.AddListener(action);
    }
}


