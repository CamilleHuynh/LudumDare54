using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [SerializeField] private GameObject m_Camera;
    private Vector3 m_InitialLocalPosition;

    [SerializeField] private float m_BobbingSpeed = 1f;
    [SerializeField] private float m_BobbingAmplitude = 0.1f;
    [SerializeField] private float m_StopLerpSpeed = 4f;

    private FPSController m_FPSController;

    private Vector2 m_CurrentInput;
    private bool m_IsWalking = false;
    private float m_Timer = 0f;
    private float m_TimeSinceWalking = 0f;
    private Vector3 m_Offset = Vector3.zero;

    private AudioSource m_AudioSource;

    private void Start()
    {
        m_FPSController = FindObjectOfType<FPSController>();

        m_InitialLocalPosition = m_Camera.transform.localPosition;

        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        m_CurrentInput = m_FPSController.CurrentInput;

        if(Mathf.Abs(m_CurrentInput.x) > 0.1f || Mathf.Abs(m_CurrentInput.y) > 0.1f)
        {
            if(!m_IsWalking)
            {
                // Started walking this frame

                // Start walkign SFX
                m_AudioSource.Play();

                m_Timer = 0f;
            }
            m_IsWalking = true;

            m_Timer += Time.deltaTime * m_BobbingSpeed;

            m_Offset.y = m_BobbingAmplitude * Mathf.Cos(m_Timer);

            m_Camera.transform.localPosition = m_InitialLocalPosition + m_Offset;
        }
        else
        {
            if(m_IsWalking)
            {
                // Stopped this frame

                // Stop walking SFX
                m_AudioSource.Stop();

                m_TimeSinceWalking = 0f;
            }
            else
            {
                m_TimeSinceWalking += Time.deltaTime;
                m_Offset.y = Mathf.Lerp(m_BobbingAmplitude * Mathf.Cos(m_Timer), 0, m_TimeSinceWalking * m_StopLerpSpeed);
            }
            m_IsWalking = false;
        }
    }
}
