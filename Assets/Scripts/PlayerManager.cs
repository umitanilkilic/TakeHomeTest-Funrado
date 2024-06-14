using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(PlayerController))]

public class PlayerManager : MonoBehaviour
{
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
            var enemyController = hit.gameObject.GetComponent<EnemyController>();
            if (enemyController.enemyLevel <= playerLevel)
            {
                enemyController.Die();
                playerController.playerAnimator.SetTrigger("Attack");
            }
            else
            {
                enemyController.AttackPlayer();
            }
            playerController.playerAnimator.SetTrigger("Attack");
        }
    }
}
