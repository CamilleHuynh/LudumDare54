using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HublotCamera : MonoBehaviour
{
    private Vector3 m_CameraOffset;
    private Quaternion m_CameraInitialRotation;

    [SerializeField] private Camera m_Camera;
    [SerializeField] private GameObject m_SubgroupLevel;

    private ReferenceManager m_References;
    private GameObject m_Head;

    private void Start()
    {
        m_References = FindObjectOfType<ReferenceManager>();
        m_Head = m_References.Camera;

        m_CameraOffset = m_SubgroupLevel.transform.position - m_References.StartLevel.transform.position;
        m_CameraInitialRotation = m_Camera.transform.rotation;
    }

    private void Update()
    {
        m_Camera.transform.rotation = m_CameraInitialRotation * m_Head.transform.rotation;
        m_Camera.transform.position = m_Head.transform.position + m_CameraOffset;
    }
}
