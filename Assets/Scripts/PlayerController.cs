using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody), typeof(PlayerManager))]
public class PlayerController : MonoBehaviour
{
    public FixedJoystick Joystick;
    public float MovementSpeed = 5f;
    public float RotationSpeed = 10f;

    private Rigidbody rb;
    public Animator playerAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

 private void FixedUpdate() 
{
    MoveAndRotateCharacter();
}

private void MoveAndRotateCharacter()
{
    float horizontalInput = Joystick.Horizontal;
    float verticalInput = Joystick.Vertical;

    Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

    if (moveDirection.magnitude > 0.01f)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, RotationSpeed * Time.fixedDeltaTime)); 

        rb.MovePosition(rb.position + moveDirection * MovementSpeed * Time.fixedDeltaTime);

        playerAnimator.SetBool("isRunning", true);
    }
    else
    {
        playerAnimator.SetBool("isRunning", false);
    }
}
}
