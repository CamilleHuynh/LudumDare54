using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [SerializeField] private GameObject m_Camera;
    private Vector3 m_InitialLocalPosition;

    [SerializeField] private float m_BobbingSpeed = 1f;
    [SerializeField] private float m_BobbingAmplitude = 0.1f;

    private FPSController m_FPSController;

    private Vector2 m_CurrentInput;
    private bool m_IsWalking = false;
    private float m_Timer = 0f;
    private Vector3 m_Offset = Vector3.zero;

    private void Start()
    {
        m_FPSController = FindObjectOfType<FPSController>();

        m_InitialLocalPosition = m_Camera.transform.localPosition;
    }

    private void Update()
    {
        m_CurrentInput = m_FPSController.CurrentInput;

        Debug.Log("current input: " + m_CurrentInput);

        if(Mathf.Abs(m_CurrentInput.x) > 0.1f || Mathf.Abs(m_CurrentInput.y) > 0.1f)
        {
            Debug.Log("Bobbing");
            m_Timer += Time.deltaTime * m_BobbingSpeed;

            m_Offset.y = m_BobbingAmplitude * Mathf.Cos(m_Timer);

            m_Camera.transform.localPosition = m_InitialLocalPosition + m_Offset;
        }
        else
        {
            // m_Camera.transform.localPosition = m_InitialLocalPosition;
        }
    }
}
