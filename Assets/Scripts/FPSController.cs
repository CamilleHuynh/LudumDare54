using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    [SerializeField] private GameObject m_Character;
    [SerializeField] private GameObject m_Body;

    [SerializeField] private CapsuleCollider m_Collider;

    [SerializeField] private float m_Height = 0f; // Places the camera

    [Header("Movement")]
    [SerializeField] private float m_MaxWalkingSpeed = 10f;
    [SerializeField] private float m_AccelerationForward = 5f;
    [SerializeField] private float m_AccelerationSide = 3f;
    [SerializeField] private float m_Decceleration = 2f;
    [SerializeField] private float m_MaxStepHeight = 0.2f;

    [Header("Collision")]
    [SerializeField] private float m_Radius = 0.5f;
    [SerializeField] private GameObject m_Feet;
    [SerializeField] private float m_DistanceRayCast = 0.05f;
    [SerializeField] private LayerMask m_ObstacleLayer;

    // Input Action
    private FPSInputAction m_InputActions;
    private InputAction m_MoveAction;
    private InputAction m_MoveSideAction;

    // Properties
    private bool m_CanMove = true;
    private Vector3 m_CurrentLocalSpeed = Vector3.zero; // Speed with x forward, y vertical, z horizontal
    private Vector3 m_CurrentSpeed = Vector3.zero;
    private Vector3 m_CurrentAcceleration = Vector3.zero;
    private List<bool> m_HasAcceleratedThisFrame = new List<bool>(3);

    private void OnEnable()
    {
        m_InputActions = new FPSInputAction();

        // Fetch action
        m_MoveAction = m_InputActions.Base.Movement;
        m_MoveAction.Enable();

        m_HasAcceleratedThisFrame.Add(false);
        m_HasAcceleratedThisFrame.Add(false);
        m_HasAcceleratedThisFrame.Add(false);
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        if(m_CanMove)
        {
            // Fetch input
            Vector2 currentInput = m_MoveAction.ReadValue<Vector2>();

            // Simple version
            Vector3 forwardMovement = m_Body.transform.forward * currentInput.y * 0.5f * m_MaxWalkingSpeed * dt;
            Vector3 sideMovement = m_Body.transform.right * currentInput.x * 0.5f *m_MaxWalkingSpeed * dt;

            // Stick to ground
            Vector3 groundNormal = Vector3.zero;
            Vector3 groundContact = m_Character.transform.position;

            if(Physics.Raycast(m_Feet.transform.position, Vector3.down, out RaycastHit hit, m_DistanceRayCast, m_ObstacleLayer))
            {
                groundNormal = hit.normal;
                groundContact = hit.point;
            }

            m_Character.transform.position = groundContact;

            m_Character.transform.position += forwardMovement + sideMovement;
        }
    }

    public void SetActiveMovement(bool value)
    {
        m_CanMove = value;
    }

    private Vector3 ClampDisplacement(Vector3 currentPosition, Vector3 displacement, Vector3 playerSpeed)
    {
        if(Physics.Raycast(currentPosition, playerSpeed.normalized, out RaycastHit hit, displacement.magnitude, m_ObstacleLayer));
        {
            return hit.point - currentPosition;
        }
    }

    private void Disable()
    {
        m_MoveAction.Disable();
        m_MoveSideAction.Disable();
    }
}
