using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasPair : MonoBehaviour
{
    public Sas LowZ;

    public Sas HighZ;

    public bool IsStartSasPair = false;

    private ReferenceManager m_References;
    private Vector3 m_InitialPosition;

    private void Start()
    {
        m_References = FindObjectOfType<ReferenceManager>();

        if(IsStartSasPair)
        {
            m_InitialPosition = this.transform.position + new Vector3(0, -1000, 0);
        }
        else
        {
            m_InitialPosition = this.transform.position;
        }
    }

    public void Activate(bool value)
    {
        if(value)
        {
            this.transform.position = m_References.StartSasPosition;
        }
        else
        {
            this.transform.position = m_InitialPosition;
        }
    }
}
