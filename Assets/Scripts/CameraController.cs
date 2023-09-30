using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private FPSInputAction m_InputActions;
    private InputAction m_RotateCamera;

    [SerializeField] private GameObject m_Body;
    [SerializeField] private GameObject m_Head;

    [SerializeField] private float m_CameraRotationSpeed = 1f;

    [SerializeField] private float m_MaxUpAngle = 85f;
    [SerializeField] private float m_MaxDownAngle = -85f;

    private bool m_CanMoveCamera = true;

    private void OnEnable()
    {
        m_InputActions = new FPSInputAction();

        // Fetch action
        m_RotateCamera = m_InputActions.Base.Camera;
        m_RotateCamera.performed += OnRotateCamera;

        m_RotateCamera.Enable();
    }

    private void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnRotateCamera(InputAction.CallbackContext context)
    {
        RotateCamera(context.ReadValue<Vector2>());
    }

    private void RotateCamera(Vector2 mouseDelta)
    {
        if(m_CanMoveCamera)
        {
            float dt = Time.deltaTime;

            // Vertical movement
            Vector3 currentRotation = m_Head.transform.eulerAngles;
            float newVerticalAngle = currentRotation.x + -1 * (mouseDelta.y * dt * m_CameraRotationSpeed);

            if(newVerticalAngle > -m_MaxDownAngle && newVerticalAngle< 180)
            {
                newVerticalAngle = -m_MaxDownAngle;
            }

            if(newVerticalAngle < (360f - m_MaxUpAngle) && newVerticalAngle > 180f)
            {
                newVerticalAngle  = (360f - m_MaxUpAngle);
            }
            m_Head.transform.eulerAngles = new Vector3(newVerticalAngle, currentRotation.y, currentRotation.z);


            // Horizontal movement

            Vector3 bodyRotation = m_Body.transform.eulerAngles;
            float newHorizontalAngle = bodyRotation.y + (mouseDelta.x * dt * m_CameraRotationSpeed);
            if(newHorizontalAngle > 360f)
            {
                newHorizontalAngle -= 360f;
            }

            if(newHorizontalAngle < 0f)
            {
                newHorizontalAngle += 360f;
            }

            m_Body.transform.eulerAngles = new Vector3(bodyRotation.x, newHorizontalAngle, bodyRotation.z);
        }
    }

    public void SetCameraMovementActive(bool value)
    {
        m_CanMoveCamera = value;
        SetCursorFree(value);
    }

    public void SetCursorFree(bool value)
    {
        if(value)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Disable()
    {
        m_RotateCamera.Disable();
    }
}
