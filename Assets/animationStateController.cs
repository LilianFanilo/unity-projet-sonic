using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isRunningHash;
    int isBoostingHash;
    int isJumpingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isBoostingHash = Animator.StringToHash("isBoosting");
        isJumpingHash = Animator.StringToHash("isJumping");

    }

    // Update is called once per frame
    void Update()
    {
        bool isBoosting = animator.GetBool(isBoostingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isJumping = animator.GetBool(isJumpingHash);

        bool forwadPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool boostPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");

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

        if (!isJumping && jumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
        }
    }
}
