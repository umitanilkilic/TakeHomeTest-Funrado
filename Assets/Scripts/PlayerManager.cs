using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(PlayerController))]

public class PlayerManager : MonoBehaviour
{
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
        PlayerLevel = 1;
    }
    public void LevelUpPlayer()
    {
        PlayerLevel++;
    }
}
