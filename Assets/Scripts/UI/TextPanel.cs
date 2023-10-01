using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPanel : MonoBehaviour
{
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        this.transform.forward = m_Camera.transform.forward;
    }
}
