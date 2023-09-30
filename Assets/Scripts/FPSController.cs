using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    [SerializeField] private CameraController m_Camera;
    [SerializeField] private float m_Height = 1f; // Places the camera

    [Header("Movement")]
    [SerializeField] private float m_MaxWalkingSpeed = 10f;
    [SerializeField] private float m_Acceleration = 5f;
    [SerializeField] private float m_Decceleration = 8f;
    [SerializeField] private float m_MaxStepHeight = 0.2f;

    [Header("Collision")]
    [SerializeField] private float m_Radius = 0.5f;

    // Input Action
    private FPSInputAction m_InputActions;
    private InputAction m_MoveForwardAction;
    private InputAction m_MoveSideAction;

    // Properties
    private bool m_CanMove = true;
    private Vector3 m_CurrentSpeed = Vector3.zero;

    private void OnEnable()
    {
        m_InputActions = new FPSInputAction();

        // Fetch action
        m_MoveForwardAction = m_InputActions.Base.Forward;
        m_MoveForwardAction.performed += OnMoveForward;
        m_MoveForwardAction.Enable();

        m_MoveSideAction = m_InputActions.Base.Side;
        m_MoveSideAction.performed += OnMoveSide;
        m_MoveSideAction.Enable();
    }

    private void OnMoveForward(InputAction.CallbackContext context)
    {

    }

    private void OnMoveSide(InputAction.CallbackContext context)
    {

    }

    public void SetActiveMovement(bool value)
    {
        m_CanMove = value;
    }

    private void Disable()
    {
        m_MoveForwardAction.Disable();
        m_MoveSideAction.Disable();
    }
}
