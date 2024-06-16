using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


[RequireComponent(typeof(PlayerController))]

public class PlayerManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController playerController;
    public TextMeshProUGUI levelText;
    public int playerLevel = 1;

    private void Start()
    {
        playerLevel = 10;
        gameManager.PlayerLevelUp(playerLevel);
    }
    public void LevelUpPlayer()
    {
        playerLevel++;
        levelText.text = $"Lv. {playerLevel}";
        gameManager.PlayerLevelUp(playerLevel);
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController.enemyLevel <= playerLevel)
            {
                enemyController.Die();
                playerController.playerAnimator.SetTrigger("Attack");
            }
        }
    }

    public void Die()
    {
        playerController.disableControls = true;
        playerController.playerAnimator.Play("Death");
        gameManager.PlayerDeath();
    }
}
