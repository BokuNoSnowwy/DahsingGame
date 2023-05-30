using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SavingService.SaveGame("LevelObjectives.json");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SavingService.LoadGame("LevelObjectives.json");
        }
    }
}
