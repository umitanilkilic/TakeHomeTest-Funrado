using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    public bool isOneTimeTrigger;
    public UnityEvent triggerEvent;

    private void Start()
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
