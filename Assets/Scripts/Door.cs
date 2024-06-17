using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = false;

    public string doorId;
    public GameObject leftDoor;
    public GameObject rightDoor;

    private Rigidbody leftDoorRb;
    private Rigidbody rightDoorRb;

    private void OnEnable() {
        GameManager.Instance.OnKeyCollected.AddListener(OnNotify);
    }



    void Start()
    {
        leftDoorRb = leftDoor.GetComponent<Rigidbody>();
        rightDoorRb = rightDoor.GetComponent<Rigidbody>();

        if (isLocked)
        {
            leftDoorRb.isKinematic = true;
            rightDoorRb.isKinematic = true;
        }
    }

    public void OnNotify(string keyId)
    {
        if (keyId == doorId && isLocked)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        leftDoorRb.isKinematic = false;
        rightDoorRb.isKinematic = false;
    }
}
