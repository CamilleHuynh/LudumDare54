using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class EntityAudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField] private List<AudioClip> m_Respirations;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    [YarnCommand("respiration")]
    public void Respiration()
    {
        int index = Random.Range(0, m_Respirations.Count);

        StartCoroutine(PlayClip(index));
    }

    private IEnumerator PlayClip(int index)
    {
        m_AudioSource.clip = m_Respirations[index];

        yield return null;

        m_AudioSource.Play();
    }
}
