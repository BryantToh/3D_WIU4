using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private float m_sensX, m_sensY;
    private float m_xRotation,m_yRotation;

    [Header("Camera Direction")]
    [SerializeField] private Transform m_camOrientation;

    [Header("Keybinds")]
    [SerializeField] private KeyCode m_cursorLockKey;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleCursor();
        CameraLook();
    }

    private void CameraLook()
    {
        float m_mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * m_sensX;
        float m_mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * m_sensY;

        m_yRotation += m_mouseX;
        m_xRotation -= m_mouseY;

        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        // Rotate the camera and orientation
        transform.rotation = Quaternion.Euler(m_xRotation, m_yRotation, 0);
        m_camOrientation.rotation = Quaternion.Euler(0, m_yRotation, 0);
    }
    private void ToggleCursor()
    {
        if (Input.GetKeyDown(m_cursorLockKey))
        {   
            // Toggles the cursor to be either visible or not visible
            Cursor.visible = !Cursor.visible;
        }
    }
}
