using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_FadeToBlack;

    [SerializeField] private float m_FadeDuration = 3f;

    private void Start()
     {
        m_FadeToBlack.alpha = 0;
     }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(EndCo());
    }

    private IEnumerator EndCo()
    {
        float timer = 0;
        float rate = 0;

        while(timer < m_FadeDuration)
        {
            yield return null;
            timer += Time.deltaTime;
            rate = timer / m_FadeDuration;

            m_FadeToBlack.alpha = rate;
        }

        Application.Quit();
    }
}
