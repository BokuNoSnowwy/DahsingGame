using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    private TextMeshProUGUI textTitle;
    private GameManager gameManager;

    
    [SerializeField] private Image[] objectivesArray = new Image[3];
    [SerializeField] private TextMeshProUGUI[] textObjectivesArray = new TextMeshProUGUI[3];

    [SerializeField] private Button launchLevelButton;

    [SerializeField] private Sprite spriteObjectiveNotAchieved; 
    [SerializeField] private Sprite spriteObjectiveDone;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        launchLevelButton.onClick.AddListener(gameManager.LaunchLevel);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Setup level informations
    public void SetupPanelForLevel()
    {
        gameObject.SetActive(true);
        
        //TODO Create sentences depending of the objectives
        //TODO Update the stars if objectives are done

        List<Objective> listObjectives = gameManager.GetActualLevel().GetObjectives();

        for (int i = 0; i < listObjectives.Count; i++)
        {
            objectivesArray[i].sprite = listObjectives[i].ObjectiveDone ? spriteObjectiveDone : spriteObjectiveNotAchieved;
            textObjectivesArray[i].text = listObjectives[i].ObjectiveString;
        }
        
        
    }
}
