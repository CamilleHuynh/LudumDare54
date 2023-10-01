using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public GameObject Character;
    public GameObject Body;
    public GameObject Camera;

    [HideInInspector] public Vector3 StartWagonPosition;
    public WagonPair StartWagonPair;
    [HideInInspector] public Vector3 StartSasPosition;
    public SasPair StartSasPair;

    [HideInInspector] public WagonPair CurrentWagonPair;
    [HideInInspector] public SasPair CurrentSasPair;

    private void Start()
    {
        StartWagonPosition = StartWagonPair.transform.position;
        StartSasPosition = StartSasPair.transform.position;

        CurrentWagonPair = StartWagonPair;
        CurrentSasPair = StartSasPair;

        // StartWagonPair.Activate(true);
        // StartSasPair.Activate(true);
    }
}
