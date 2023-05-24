using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameLevelPanel : LevelPanel
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

    public override void SetupPanelForLevel()
    {
        base.SetupPanelForLevel();
    }
}
