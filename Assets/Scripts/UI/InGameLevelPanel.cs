using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameLevelPanel : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton; 
    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(GameManager.Instance.ReturnToLobby);
    }

    public void DisplayPanel()
    {
        gameObject.SetActive(true);
    }
}
