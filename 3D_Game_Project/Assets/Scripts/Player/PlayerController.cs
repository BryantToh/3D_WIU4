using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform m_cameraTransform;
    private CharacterController m_characterController;

    [Header("Movement Stats")]
    [SerializeField] private float m_movementSpeed;
    private Vector3 m_playerVelocity;

    [Header("Jumping Stats")]
    [SerializeField] private float m_jumpHeight;
    private bool m_isGround;
    private const float k_gravity = -9.81f;

    [Header("Interactbles")]
    [SerializeField] private List<GameObject> m_interactableGO;

    [Header("KeyBinds")]
    [SerializeField] private KeyCode m_jumpKey;
    [SerializeField] private KeyCode m_interaction;

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_interactableGO = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Interaction();
    }

    private void Movement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        m_characterController.Move(move * Time.deltaTime * m_movementSpeed); 
    }

    private void Jump()
    {
        m_isGround = m_characterController.isGrounded;

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

    private void Interaction()
    {
        // Shoots a ray from the camera position
        Ray ray = new Ray(m_cameraTransform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10);
        //foreach (GameObject interactables in m_interactableGO)
        //{
            
        //}
    }
}
