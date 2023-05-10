using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInstanciator : MonoBehaviour
{
    [Header("Prefabs/Objects")]
    public GameObject poolPrefab;
    public GameObject swipePrefab;
    public GameObject touchPrefab;

    private GameObject poolObject;
    private GameObject swipeObject;
    private GameObject touchObject;

    [Header("Buttons")]
    public Button poolButton;
    public Button touchButton;
    public Button swipeButton;

    [Header("Transform")]
    public Transform spawnPosition;

    [Header("Ghost Update")]
    public UnityEvent changePlayerEnvent;
    
    // Start is called before the first frame update
    void Start()
    {
        poolButton.onClick.AddListener(InstanciatePool);
        swipeButton.onClick.AddListener(InstanciateSwipe);
        touchButton.onClick.AddListener(InstanciateTouch);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstanciatePool()
    {
        Time.timeScale = 1;
        if (swipeObject)
        {
            swipeObject.SetActive(false);
        }

        if (touchObject)
        {
            touchObject.SetActive(false);
        }
        
        if (poolObject != null)
        {
            poolObject.SetActive(true);
            poolObject.transform.position = spawnPosition.position;
        }
        else
        {
            poolObject = Instantiate(poolPrefab, transform.position, Quaternion.identity);
            poolObject.transform.position = spawnPosition.position;
        }
        
        changePlayerEnvent.Invoke();
    }
    
    public void InstanciateTouch()
    {
        Time.timeScale = 1;
        if (swipeObject)
        {
            swipeObject.SetActive(false);
        }

        if (poolObject)
        {
            poolObject.SetActive(false);
        }
        
        if (touchObject != null)
        {
            touchObject.SetActive(true);
            touchObject.transform.position = spawnPosition.position;
        }
        else
        {
            touchObject = Instantiate(touchPrefab, transform.position, Quaternion.identity);
            touchObject.transform.position = spawnPosition.position;
        }
        
        changePlayerEnvent.Invoke();
    }
    
    public void InstanciateSwipe()
    {
        Time.timeScale = 1;
        if (touchObject)
        {
            touchObject.SetActive(false);
        }

        if (poolObject)
        {
            poolObject.SetActive(false);
        }
        
        if (swipeObject != null)
        {
            swipeObject.SetActive(true);
            swipeObject.transform.position = spawnPosition.position;
        }
        else
        {
            swipeObject = Instantiate(swipePrefab, transform.position, Quaternion.identity);
            swipeObject.transform.position = spawnPosition.position;
        }
        
        changePlayerEnvent.Invoke();
    }
}
