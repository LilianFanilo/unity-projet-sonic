using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class animationStateController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    Animator animator;
    int isRunningHash;
    int isBoostingHash;
    int isJumpingHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isBoostingHash = Animator.StringToHash("isBoosting");
        isJumpingHash = Animator.StringToHash("isJumping");

    }

    void Update()
    {
        bool isBoosting = animator.GetBool(isBoostingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isJumping = animator.GetBool(isJumpingHash);

        bool forwadPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool boostPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetButton("Jump");
        //bool sprintPressed = 

        if (!isRunning && forwadPressed)
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && !forwadPressed)
        {
            animator.SetBool(isRunningHash, false);
        }

        if (!isBoosting && (forwadPressed && boostPressed))
        {
            animator.SetBool(isBoostingHash, true);
        }

        if (isBoosting && (!forwadPressed || !boostPressed))
        {
            animator.SetBool(isBoostingHash, false);
        }

        if (playerController.isJumping == false)
        {
            animator.SetBool(isJumpingHash, false);
        }

        if (playerController.isJumping == true)
        {
            animator.SetBool(isJumpingHash, true);
        }
    }
}
