using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private CharacterController m_characterController;

    [Header("Movement Stats")]
    [SerializeField] private float m_movementSpeed;
    private Vector3 playerVelocity;

    [Header("Jumping Stats")]
    [SerializeField] private float m_jumpHeight;
    private bool m_isGround;
    private const float k_gravity = -9.81f;

    [Header("KeyBinds")]
    [SerializeField] private KeyCode m_jumpKey;

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

    }

    private void Movement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        m_characterController.Move(move * Time.deltaTime * m_movementSpeed); 
    }

    private void Jump()
    {
        m_isGround = m_characterController.isGrounded;

        if (m_isGround && playerVelocity.y < 0f)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetKey(m_jumpKey) && m_isGround)
        {
            Debug.Log("Is Jumping");
            playerVelocity.y += Mathf.Sqrt(m_jumpHeight * -3.0f * k_gravity);
        }

        playerVelocity.y += k_gravity * Time.deltaTime;
        m_characterController.Move(playerVelocity * Time.deltaTime);
    }

    private bool CheckGrounded()
    {
        return m_isGround;
    }
}
