using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float movementSpeed = 5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;
    public float smoothTurnTime = 0.1f;
    private float turnSmoothVelocity;

    public TextMeshProUGUI levelText;

    private CharacterController characterController;
    private int playerLevel = 1;
    private Vector3 velocity;

    public Animator playerAnimator;

    public int PlayerLevel
    {
        get => playerLevel;
        set
        {
            playerLevel = value;
            levelText.text = $"Lv. {playerLevel}";
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        levelText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        PlayerLevel = 1;
    }

    private void Update()
    {
        MoveAndRotateCharacter();
    }

    private void MoveAndRotateCharacter()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        Vector3 moveDirection = new Vector3(h, 0f, v);

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            characterController.Move(transform.forward * movementSpeed * Time.deltaTime);
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
