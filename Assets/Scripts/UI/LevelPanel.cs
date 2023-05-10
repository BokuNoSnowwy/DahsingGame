using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    private TextMeshProUGUI textTitle;

    [SerializeField] private Image objective1;
    [SerializeField] private Image objective2;
    [SerializeField] private Image objective3;

    [SerializeField] private TextMeshProUGUI textObjective1;
    [SerializeField] private TextMeshProUGUI textObjective2;
    [SerializeField] private TextMeshProUGUI textObjective3;
    // Start is called before the first frame update
    void Start()
    {
        
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

    }
}
