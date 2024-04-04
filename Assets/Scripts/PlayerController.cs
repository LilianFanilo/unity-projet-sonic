using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private const float GRAVITY_VALUE = -19.81f;

    [SerializeField] private float m_Speed = 30f;
    [SerializeField] private float m_JumpHeight = 10f;
    [SerializeField] private float m_GravityMultiplier = 2.5f;
    [SerializeField] private float m_TurnSmoothTime = 0.05f;
    [SerializeField] private float homingAttackSpeed = 100f;
    [SerializeField] private float homingAttackRange = 25f;
    [SerializeField] private AudioClip JumpSoundClip;
    [SerializeField] private AudioClip BoostSoundClip;
    [SerializeField] private AudioClip HomingAttackSoundClip;
    [SerializeField] private AudioClip DestroyEnemySoundClip;
    [SerializeField] private LayerMask enemyLayer;

    private new CinemachineFreeLook camera;
    private CharacterController m_Character;
    private float m_JumpVelocity;
    private float m_TurnSmoothVelocity;
    private float m_Gravity;
    private Vector2 m_MoveVector;
    private Transform targetEnemy;

    public bool isMoving = false;
    public bool isJumping = false;
    public bool isSprinting = false;
    public bool isAttacking = false;
    public bool isHomingAttackActive = false;

    #region Initialization
    private void OnEnable()
    {
        m_Character = gameObject.AddComponent<CharacterController>();
        m_Character.radius = 0.4f;
    }

    private void OnDisable()
    {
        Destroy(m_Character);
    }
    #endregion

    void Update()
    {
        if (m_Character.isGrounded)
        {
            isAttacking = false;
            isJumping = false;
        }

        if (isAttacking && !isHomingAttackActive)
        {
            targetEnemy = DetectNearestEnemy();

            if (targetEnemy != null)
            {
                isHomingAttackActive = true;
                AudioManager.instance.PlayClip(HomingAttackSoundClip);
            }
        }

        if (isHomingAttackActive)
        {
            HomingAttackMove();

            if (Vector3.Distance(transform.position, targetEnemy.position) < 1f)
            {
                HandleEnemyCollision();
                transform.position += Vector3.up * m_JumpHeight * 2 * Time.deltaTime;
                isHomingAttackActive = false;
                isAttacking = false;
            }
        }

        if (isHomingAttackActive == false)
        {
            Move();
            ApplyGravity();
        }
    }

    void Start()
    {
        camera = GetComponent<CinemachineFreeLook>();
    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        bool boostPressed = Input.GetKey("left shift") || Input.GetKey("e");
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y);

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            targetAngle += Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3. forward;

            if (boostPressed)
            {
                m_Character.Move(moveDirection.normalized * (m_Speed * 2.5f) * Time.deltaTime);
            }
            else
            {
                m_Character.Move(moveDirection.normalized * m_Speed * Time.deltaTime);
            }
        }
    }

    public void Jump()
    {
        isJumping = true;
        m_Gravity = GRAVITY_VALUE * m_GravityMultiplier;

        if (!m_Character.isGrounded)
        {
            return;
        }

        m_JumpVelocity += Mathf.Sqrt(m_JumpHeight * -3 * m_Gravity);

        m_Character.Move(Vector3.up * m_JumpHeight * Time.deltaTime);

        AudioManager.instance.PlayClip(JumpSoundClip);

    }

    private void ApplyGravity()
    {
        m_Gravity = GRAVITY_VALUE * m_GravityMultiplier;

        if (m_Character.isGrounded && m_JumpVelocity < 0)
        {
            m_JumpVelocity = 0f;
            return;
        }

        if (!m_Character.isGrounded && m_JumpVelocity > 0 && Input.GetButton("Jump") == false)
        {
            m_Gravity *= 2;
        }

        m_JumpVelocity += m_Gravity * Time.deltaTime ;

        m_Character.Move(Vector3.up * m_JumpVelocity * Time.deltaTime);
    }

    Transform DetectNearestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, homingAttackRange, enemyLayer);

        if (enemies.Length > 0)
        {
            Transform nearestEnemy = enemies[0].transform;
            float closestDistance = Vector3.Distance(transform.position, nearestEnemy.position);

            foreach (var enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    nearestEnemy = enemy.transform;
                    closestDistance = distance;
                }
            }

            return nearestEnemy;
        }

        return null;
    }

    public void HomingAttackMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, homingAttackSpeed * Time.deltaTime);
    }

    public void HandleEnemyCollision()
    {
        AudioManager.instance.PlayClip(DestroyEnemySoundClip);
    }

    public void ResetCamera()
    {
        camera.m_RecenterToTargetHeading.m_enabled = true;
    }

    public void Attack()
    {
        if (!m_Character.isGrounded)
        {
            isAttacking = true;
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        bool sprintPressed = context.started || context.performed;
        if (sprintPressed)
        {
            isSprinting = true;
        } else
        {
            isSprinting = false;
        }
    }

    public bool PublicIsSprinting()
    {
        return isSprinting; 
    }
}
