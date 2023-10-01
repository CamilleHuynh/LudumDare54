using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassThroughCollider : MonoBehaviour
{
    public event Action OnColliderEnter;
    public event Action OnColliderExit;

    private void OnTriggerEnter(Collider other)
    {
        OnColliderEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnColliderExit?.Invoke();
    }
}
