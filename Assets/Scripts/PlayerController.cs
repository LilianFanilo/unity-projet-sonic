using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private const float GRAVITY_VALUE = -19.81f;

    [SerializeField] private float m_Speed = 5.0f;
    [SerializeField] private float m_JumpHeight = 5f;
    [SerializeField] private float m_GravityMultiplier = 2f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;

    private CharacterController m_Character;
    private float m_JumpVelocity;
    private float m_TurnSmoothVelocity;
    private float m_Gravity;

    private Vector2 m_MoveVector;

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

    private void FixedUpdate()
    {
        Move();

        ApplyGravity();
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


        //if (!m_Character.isGrounded && m_JumpVelocity > 0 && Input.GetButton("Jump") == true)
        //{
        //    Debug.Log("long jump");
        //    m_JumpHeight = 5f;
        //}

        //if (!m_Character.isGrounded && m_JumpVelocity > 0 && Input.GetButton("Jump") == false)
        //{
            //Debug.Log("short jump");
            //m_JumpHeight /= 2;

        //} 

        if (!m_Character.isGrounded)
        {
            return;
        }

        m_JumpVelocity += Mathf.Sqrt(m_JumpHeight * -3 * m_Gravity);

        m_Character.Move(Vector3.up * m_JumpHeight * Time.deltaTime);

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
            //Debug.Log("short jump");
            m_Gravity *= 2;
        }

        m_JumpVelocity += m_Gravity * Time.deltaTime ;

        m_Character.Move(Vector3.up * m_JumpVelocity * Time.deltaTime);
    }
}
