using UnityEngine;
using UnityEngine.UI;

public class LevelPointMap : MonoBehaviour
{
    public int indexLevel;

    [SerializeField] private LevelPanel levelSelectionPanel;
    private Button button;


    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetupIndexGameplayManager);
    }
    
    private void SetupIndexGameplayManager()
    {
        GameManager.Instance.IndexLevel = indexLevel;
        
        levelSelectionPanel.SetupPanelForLevel(indexLevel);
    }
}
