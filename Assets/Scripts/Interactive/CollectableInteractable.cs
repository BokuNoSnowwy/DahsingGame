using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private GameManager gameManager;
    private Level gameLevel;
    private Collider2D collider2D;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameLevel = gameManager.GetActualLevel();
        collider2D = GetComponent<Collider2D>();
        
                
        gameManager.AddListenerPlayerRespawn(ResetInteractable);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        collider2D.enabled = !gameLevel.objectiveCollectable.ObjectiveDone;
        spriteRenderer.sprite = gameLevel.objectiveCollectable.ObjectiveDone? gameLevel.objectiveCollectable.ObtainedSprite : gameLevel.objectiveCollectable.BaseSprite;
        spriteRenderer.color = gameLevel.objectiveCollectable.ObjectiveDone? gameLevel.objectiveCollectable.ObtainedSpriteColor : gameLevel.objectiveCollectable.BaseSpriteColor;
    }

    public void DetectPlayer(Movement playerMovement)
    {
        Debug.LogError("DetectPlayer");
        playerMovement.hasDashed = false;
        spriteRenderer.sprite = GameManager.Instance.GetActualLevel().objectiveCollectable.ObtainedSprite;
        spriteRenderer.color = GameManager.Instance.GetActualLevel().objectiveCollectable.ObtainedSpriteColor;
        GameManager.Instance.collectableEvent.Invoke();
        
    }
    
    public Vector2 iniPos { get; set; }
    
    public void ResetInteractable()
    {
        Initialization();
    }

    public void ListenEventGameManager()
    {
        throw new System.NotImplementedException();
    }

}
