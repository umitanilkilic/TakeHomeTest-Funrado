using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerManager = other.GetComponent<PlayerManager>();
            playerManager.gameManager.AddKey(keyId);
            Destroy(gameObject);
        }
    }
}