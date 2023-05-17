using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelPointMap : MonoBehaviour
{
    public int indexLevel;

    [SerializeField] private LevelPanel levelSelectionPanel;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetupIndexGameplayManager);
    }
    
    private void SetupIndexGameplayManager()
    {
        GameManager.Instance.IndexLevel = indexLevel;
        
        levelSelectionPanel.SetupPanelForLevel();
    }
}
