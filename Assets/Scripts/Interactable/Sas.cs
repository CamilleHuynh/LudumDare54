using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sas : MonoBehaviour
{
    private ReferenceManager m_References;
    private Vector3 m_InitialPosition;

    public bool IsStartSas = false;
    public bool IsStartGame = false;

    [Header("Loop")]
    [SerializeField] private PassThroughCollider m_TPCollider;
    [SerializeField] private Sas m_CoupledSas;

    [Header("Change wagon")]
    [SerializeField] private PassThroughCollider m_SwitchWagonCollider;
    [SerializeField] private WagonPair m_NextWagonPair;

    private void Start()
    {
        m_InitialPosition = this.transform.position;

        m_References = FindObjectOfType<ReferenceManager>();

        m_TPCollider.OnColliderEnter += TPCollider_OnColliderEnter;
        m_SwitchWagonCollider.OnColliderEnter += SwitchWagonCollider_OnColliderEnter;
    }

    private void TPCollider_OnColliderEnter()
    {
        m_References.Character.transform.position = m_CoupledSas.transform.position + (m_References.Character.transform.position - this.transform.position);
    }

    private void SwitchWagonCollider_OnColliderEnter()
    {
        if(!IsStartGame)
        {
            m_References.CurrentWagonPair.Activate(false);
            m_NextWagonPair.Activate(true);

            m_References.CurrentWagonPair = m_NextWagonPair;
        }
    }
}
