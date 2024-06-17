using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    public float resetTime = 4f;


    public UnityEvent OnPlayerDeath;
    public UnityEvent<int> OnPlayerLevelUp;

    public UnityEvent<string> OnKeyCollected;

    public UnityEvent<string> On;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
        Debug.Log($"Player died, restarting game in {resetTime} seconds...");
        Invoke("RestartGame", resetTime);
    }

    public void PlayerLevelUp(int level)
    {
        OnPlayerLevelUp?.Invoke(level);
    }

    public void AddKey(string keyId)
    {
        OnKeyCollected?.Invoke(keyId);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
