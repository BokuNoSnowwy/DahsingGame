using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelList : MonoBehaviour
{
    public void GoToLevelList()
    {
        GameObject levelContents = FindObjectOfType<ScrollRect>().transform.GetChild(0).gameObject;
        for (int i = 0; i <= GameManager.Instance.lvlUnlock; i++)
        {
            Debug.Log(i);
            if (i != 0)
                levelContents.transform.GetChild(i).transform.GetChild(5).gameObject.SetActive(false);
        }
    }
}
