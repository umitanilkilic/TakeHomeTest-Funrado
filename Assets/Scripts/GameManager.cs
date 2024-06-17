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
        Debug.Log("Player Died");
        OnPlayerDeath?.Invoke();
        Invoke("RestartGame", 2f);
    }

    public void PlayerLevelUp(int level)
    {
        OnPlayerLevelUp?.Invoke(level);
    }

    public void AddKey(string keyId)
    {
        OnKeyCollected?.Invoke(keyId);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
