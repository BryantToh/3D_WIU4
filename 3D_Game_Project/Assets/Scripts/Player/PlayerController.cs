using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform m_cameraOrientation;
    private CharacterController m_characterController;

    [Header("Movement Stats")]
    [SerializeField] private float m_movementSpeed;
    private Vector3 m_playerVelocity;
    private Vector3 m_moveDirection;

    [Header("Jumping Stats")]
    [SerializeField] private float m_jumpHeight;
    private bool m_isGround;
    private const float k_gravity = -9.81f;

    [Header("Crouching Stats")]
    private bool m_isCrouching;

    [Header("Interactions")]
    [SerializeField] private float m_interactRange;

    [Header("KeyBinds")]
    [SerializeField] private KeyCode m_jumpKey;
    [SerializeField] private KeyCode m_crouchKey;
    [SerializeField] private KeyCode m_interactionKey;

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Crouching();
        
        Interaction();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Allows movement direction to be controlled by the camera direction
        m_moveDirection = m_cameraOrientation.forward * verticalInput + m_cameraOrientation.right * horizontalInput;

        m_characterController.Move(m_moveDirection.normalized * Time.deltaTime * m_movementSpeed);
    }

    private void Jump()
    {
        m_isGround = m_characterController.isGrounded;

        // Ensure that it will stay grounded
        if (m_isGround && m_playerVelocity.y < 0f)
        {
            m_playerVelocity.y = 0f;
        }

        if (Input.GetKey(m_jumpKey) && m_isGround)
        {
            Debug.Log("Is Jumping");
            m_playerVelocity.y += Mathf.Sqrt(m_jumpHeight * -3.0f * k_gravity);
        }

        m_playerVelocity.y += k_gravity * Time.deltaTime;
        m_characterController.Move(m_playerVelocity * Time.deltaTime);
    }

    private void Crouching()
    {
        if (Input.GetKeyDown(m_crouchKey))
        {
            m_isCrouching = !m_isCrouching;

            // If-else statement (if true then height value will be 1 else 2)
            m_characterController.height = m_isCrouching ? 1 : 2;
        }
        else if (Input.GetKeyUp(m_crouchKey) && m_isCrouching)
        {
            m_isCrouching = false;
            m_characterController.height = 2;
        }
    }

    private void Interaction()
    {
        if(Input.GetKeyDown(m_interactionKey))
        {
            Ray r = new Ray(m_cameraOrientation.position, m_cameraOrientation.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, m_interactRange))
            {
                if(hitInfo.collider.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
            Debug.DrawRay(r.origin, r.direction * m_interactRange, Color.red, 1f);
        }
    }
}
