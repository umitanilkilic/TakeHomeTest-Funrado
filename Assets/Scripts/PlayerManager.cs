using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


[RequireComponent(typeof(PlayerController))]

public class PlayerManager : MonoBehaviour
{
    public Action OnPlayerDeath;
    public Action OnPlayerLevelUp;
    public PlayerController playerController;
    public TextMeshProUGUI levelText;
    private int playerLevel = 1;
    public int PlayerLevel
    {
        get => playerLevel;
        set
        {
            playerLevel = value;
            levelText.text = $"Lv. {playerLevel}";
        }
    }
    private void Start()
    {
        PlayerLevel = 10;
    }
    public void LevelUpPlayer()
    {
        PlayerLevel++;
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
        playerController.playerAnimator.Play("Death");
        OnPlayerDeath?.Invoke();
    }

    public void LevelUp()
    {
        PlayerLevel++;
        OnPlayerLevelUp?.Invoke();
    }
}
