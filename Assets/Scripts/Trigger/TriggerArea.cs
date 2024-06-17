using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public bool isOneTimeTrigger;
    public UnityEvent triggerEvent;

    void Start()
    {
        if(isOneTimeTrigger){
            triggerEvent.AddListener(() => {
                Destroy(gameObject);
            });
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            triggerEvent.Invoke();
        }
    }

}
