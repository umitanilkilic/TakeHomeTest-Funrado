using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager.OnPlayerDeath += PlayerManager_OnPlayerDeath;
        playerManager.OnPlayerLevelUp += PlayerManager_OnPlayerLevelUp;
    }

    private void PlayerManager_OnPlayerDeath()
    {
        Debug.Log("Player died");
    }

    private void PlayerManager_OnPlayerLevelUp()
    {
        Debug.Log("Player leveled up");
    }
}
