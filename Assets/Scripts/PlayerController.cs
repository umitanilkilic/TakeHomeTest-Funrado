using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float gravity = 9.8f;
    public float speed = 3.0f;
    private CharacterController characterController;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        var velocity = new Vector3(joystick.Horizontal,-gravity,joystick.Vertical);
        characterController.Move(velocity*Time.deltaTime*speed);
    }
}
