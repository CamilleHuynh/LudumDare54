using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    private ReferenceManager m_References;
    private Vector3 m_InitialPosition;

    public bool IsStartWagon = false;
    public bool IsStartGame = false;

    [Header("Change wagon")]
    [SerializeField] private PassThroughCollider m_SwitchSasCollider;
    [SerializeField] private SasPair m_NextSasPair;

    private void Start()
    {
        m_InitialPosition = this.transform.position;

        m_References = FindObjectOfType<ReferenceManager>();

        m_SwitchSasCollider.OnColliderEnter += SwitchSasCollider_OnTriggerEnter;

        if(IsStartWagon)
        {
            m_InitialPosition = this.transform.position + new Vector3(0, -500, 0);
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

    private void SwitchSasCollider_OnTriggerEnter()
    {
        if(!IsStartGame)
        {
            m_References.CurrentSasPair.Activate(false);
            m_NextSasPair.Activate(true);

            m_References.CurrentSasPair = m_NextSasPair;
        }
    }
}
