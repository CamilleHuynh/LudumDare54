using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonPair : MonoBehaviour
{
    public Wagon MainWagon;
    public Wagon SideWagon;

    public bool IsStartWagonPair = false;

    private ReferenceManager m_References;
    private Vector3 m_InitialPosition;

    private void Start()
    {
        m_References = FindObjectOfType<ReferenceManager>();

        if(IsStartWagonPair)
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
            this.transform.position = m_References.StartWagonPosition;
        }
        else
        {
            this.transform.position = m_InitialPosition;
        }
    }
}
