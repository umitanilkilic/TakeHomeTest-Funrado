using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


[RequireComponent(typeof(PlayerController))]

public class PlayerManager : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI levelText;
    public int playerLevel = 1;

    private void Start()
    {
        UpdateLevelText();
        NotifyGameManager();
    }
    public void LevelUpPlayer()
    {
        playerLevel++;
        UpdateLevelText();
        NotifyGameManager();
    }

    private void UpdateLevelText()
    {
        levelText.text = $"Lv. {playerLevel}";
    }

    private void NotifyGameManager()
    {
        GameManager.Instance.PlayerLevelUp(playerLevel);
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
        GetComponent<Rigidbody>().isKinematic = true;
        playerController.disableControls = true;
        playerController.playerAnimator.Play("Death");
        GameManager.Instance.PlayerDeath();
    }
}
