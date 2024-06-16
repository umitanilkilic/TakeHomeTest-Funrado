using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnPlayerDeath;
    public UnityEvent<int> OnPlayerLevelUp;

    public UnityEvent<string> OnKeyCollected;

    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public void PlayerLevelUp(int level)
    {
        OnPlayerLevelUp?.Invoke(level);
    }

    public void AddKey(string keyId)
    {
        OnKeyCollected?.Invoke(keyId);
    }
}
