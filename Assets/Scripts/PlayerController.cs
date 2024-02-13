using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private const float GRAVITY_VALUE = -19.81f;

    [SerializeField] private float m_Speed = 5.0f;
    [SerializeField] private float m_JumpHeight = 5f;
    [SerializeField] private float m_GravityMultiplier = 2f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;
    [SerializeField] private float homingAttackSpeed = 10f;
    [SerializeField] private float homingAttackRange = 5f;

    private new CinemachineFreeLook camera;
    private CharacterController m_Character;
    private float m_JumpVelocity;
    private float m_TurnSmoothVelocity;
    private float m_Gravity;

    private Vector2 m_MoveVector;

    //
    public LayerMask enemyLayer;

    private bool isHomingAttackActive = false;
    private Transform targetEnemy;

    void Update()
    {
        if (!m_Character.isGrounded && Input.GetKeyDown("l") && !isHomingAttackActive)
        {
            // Détecter l'ennemi le plus proche
            Debug.Log("Detect enemy");

            targetEnemy = DetectNearestEnemy();

            Debug.Log("Enemy is detected or not ?");


            if (targetEnemy != null)
            {
                Debug.Log("Enemy is detected !");

                // Activer la homing attack
                isHomingAttackActive = true;
            }
        }

        if (isHomingAttackActive)
        {
            Debug.Log("Sonic attack");

            // Déplacer Sonic vers l'ennemi ciblé
            HomingAttackMove();

            // Gérer la collision avec l'ennemi
            if (Vector3.Distance(transform.position, targetEnemy.position) < 1f)
            {
                HandleEnemyCollision();
                isHomingAttackActive = false; // Désactiver la homing attack après la collision
            }
        }
    }

    Transform DetectNearestEnemy()
    {
        Debug.Log("position de l'enemi");

        Collider[] enemies = Physics.OverlapSphere(transform.position, homingAttackRange, enemyLayer);

        if (enemies.Length > 0)
        {
            Debug.Log("1 ou plusieurs enemis");

            Transform nearestEnemy = enemies[0].transform;
            float closestDistance = Vector3.Distance(transform.position, nearestEnemy.position);

            foreach (var enemy in enemies)
            {
                Debug.Log("position de chaque enemi");

                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    Debug.Log("enemi le plus proche");

                    nearestEnemy = enemy.transform;
                    closestDistance = distance;
                }
            }

            return nearestEnemy;
        }

        return null;
    }

    void HomingAttackMove()
    {
        // Déplacer Sonic vers l'ennemi ciblé
        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, homingAttackSpeed * Time.deltaTime);
    }

    void HandleEnemyCollision()
    {
        // Logique de gestion de collision avec l'ennemi
        Debug.Log("Homing attack collision with enemy!");
        // Vous pouvez détruire l'ennemi, réduire sa santé, etc.
    }
    //

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

    void Start()
    {
        camera = GetComponent<CinemachineFreeLook>();
    }

    private void FixedUpdate()
    {
        Move();

        if (isHomingAttackActive == false)
        {
            ApplyGravity();
        }
    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(m_MoveVector.x, 0f, m_MoveVector.y);

        if (direction.magnitude >= 0.1f)
        {
            //TODO: Get direction angle from direction vector

            //TODO: Apply direction angle to transform rotation

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            targetAngle += Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //TODO: Convert direction angle into a movement vector

            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3. forward;

            m_Character.Move(moveDirection.normalized * m_Speed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        m_Gravity = GRAVITY_VALUE * m_GravityMultiplier;

        if (!m_Character.isGrounded)
        {
            return;
        }

        m_JumpVelocity += Mathf.Sqrt(m_JumpHeight * -3 * m_Gravity);

        m_Character.Move(Vector3.up * m_JumpHeight * Time.deltaTime);

    }

    public void Attack()
    {
  
    }

    private void ApplyGravity()
    {
        //TODO: Increment velocity with gravity only when character is grounded
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

    public void ResetCamera()
    {
        camera.m_RecenterToTargetHeading.m_enabled = true;
    }
}
