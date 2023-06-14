using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    public void ResetLevel()
    {
        GameManager.Instance.RespawnPlayer();
    }

    public void BackHome()
    {
        GameManager.Instance.ReturnToLobby();
    }
}
