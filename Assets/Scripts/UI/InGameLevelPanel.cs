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
        SetupPanelForLevel(0);
        gameObject.SetActive(true);
    }

    public override void SetupPanelForLevel(int index)
    {
        List<Objective> listObjectives = gameManager.GetActualLevel().GetObjectives();
        
        textTitle.text = /*"Level " +*/ gameManager.GetActualLevel().sceneName + " Finished !";
        textTitle.rectTransform.anchorMax = new Vector2(.5f, .5f);
        textTitle.rectTransform.anchorMin = new Vector2(.5f, .5f);

        for (int i = 0; i < listObjectives.Count; i++)
        {
            objectivesArray[i].sprite = listObjectives[i].Success ? spriteObjectiveDone : spriteObjectiveNotAchieved;
        }

        for (int i = 0; i < textObjectivesArray.Length; i++)
        {
            textObjectivesArray[i].text = listObjectives[i].ObjectiveString;
        }
    }
}
