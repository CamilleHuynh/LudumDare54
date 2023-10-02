using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public GameObject Character;
    public GameObject Body;
    public GameObject Camera;
    public AudioSource m_MusicSource;

    public GameObject StartLevel;
    [HideInInspector] public Vector3 StartWagonPosition;
    public Wagon StartWagon;
    [HideInInspector] public Vector3 StartSasPosition;
    public SasPair StartSasPair;

    [HideInInspector] public Wagon CurrentWagon;
    [HideInInspector] public SasPair CurrentSasPair;

    private void Start()
    {
        StartWagonPosition = StartWagon.transform.position;
        StartSasPosition = StartSasPair.transform.position;

        CurrentWagon = StartWagon;
        CurrentSasPair = StartSasPair;

        // StartWagonPair.Activate(true);
        // StartSasPair.Activate(true);
    }
}
