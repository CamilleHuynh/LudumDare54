using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private FPSInputAction m_InputActions;
    private InputAction m_RotateCamera;

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

            Vector3 currentRotation = this.transform.eulerAngles;

            // Vertical movement
            float newVerticalAngle = currentRotation.x + -1 * (mouseDelta.y * dt * m_CameraRotationSpeed);

            if(newVerticalAngle > -m_MaxDownAngle && newVerticalAngle< 180)
            {
                newVerticalAngle = -m_MaxDownAngle;
            }

            if(newVerticalAngle < (360f - m_MaxUpAngle) && newVerticalAngle > 180f)
            {
                newVerticalAngle  = (360f - m_MaxUpAngle);
            }

            // Horizontal movement
            float newHorizontalAngle = currentRotation.y + (mouseDelta.x * dt * m_CameraRotationSpeed);
            if(newHorizontalAngle > 360f)
            {
                newHorizontalAngle -= 360f;
            }

            if(newHorizontalAngle < 0f)
            {
                newHorizontalAngle += 360f;
            }

            // Set new angles
            this.transform.eulerAngles = new Vector3(newVerticalAngle, newHorizontalAngle, currentRotation.z);
        }
    }

    public void SetCameraMovementActive(bool value)
    {
        m_CanMoveCamera = value;
    }

    private void Disable()
    {
        m_RotateCamera.Disable();
    }
}
