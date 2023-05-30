using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameLevelPanel : LevelPanel
{
    [SerializeField] private Button nextLevelButton;
    
    void Start()
    {
        
    }
    
    public void Initialization()
    {
        gameManager = GameManager.Instance;
        nextLevelButton.onClick.AddListener(gameManager.ReturnToLobby);
    }
    

    public void DisplayPanel()
    {
        Initialization();
        SetupPanelForLevel();
        gameObject.SetActive(true);
    }

    public override void SetupPanelForLevel()
    {
        Debug.LogError(gameManager);
        List<Objective> listObjectives = gameManager.GetActualLevel().GetObjectives();
        
        textTitle.text = "Level " + gameManager.GetActualLevel().sceneName + " Finished !";

        for (int i = 0; i < listObjectives.Count; i++)
        {
            objectivesArray[i].sprite = listObjectives[i].Success ? spriteObjectiveDone : spriteObjectiveNotAchieved;
        }
    }
}
