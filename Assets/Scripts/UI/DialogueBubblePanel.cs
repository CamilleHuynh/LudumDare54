using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBubblePanel : MonoBehaviour
{
    private ReferenceManager m_References;
    private GameObject m_Body;

    private void Start()
    {
        m_References = FindObjectOfType<ReferenceManager>();
        m_Body = m_References.Body;
    }

    private void Update()
    {
        this.transform.forward = m_Body.transform.forward;
    }
}
