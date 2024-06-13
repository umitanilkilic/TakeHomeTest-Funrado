using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController), typeof(PlayerManager))]
public class PlayerController : MonoBehaviour
{
    public FixedJoystick Joystick;
    public float MovementSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float smoothTurnTime = 0.1f;
    private float turnSmoothVelocity;
    private CharacterController characterController;
    private Vector3 velocity;

    public Animator playerAnimator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveAndRotateCharacter();
    }

    private void MoveAndRotateCharacter()
    {
        float h = Joystick.Horizontal;
        float v = Joystick.Vertical;

        Vector3 moveDirection = new Vector3(h, 0f, v);

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            characterController.Move(transform.forward * MovementSpeed * Time.deltaTime);
            playerAnimator.Play("Run");
        }
        else
        {
            playerAnimator.Play("Idle");
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

}
