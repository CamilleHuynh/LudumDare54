using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class EntityAudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField] private List<AudioClip> m_Respirations;

    [YarnCommand("respiration")]
    public void Respiration(int index)
    {
        if(index >= 0 && index < m_Respirations.Count)
        {
            m_AudioSource.clip = m_Respirations[index];
            m_AudioSource.Play();
        }
    }
}
