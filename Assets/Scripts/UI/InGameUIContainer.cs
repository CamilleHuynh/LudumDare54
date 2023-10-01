using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIContainer : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_CanvasGroup;

    private void Start()
    {
        m_CanvasGroup.interactable = false;
        m_CanvasGroup.blocksRaycasts = false;

        Show(false);
    }

    public void Show(bool value)
    {
        m_CanvasGroup.alpha = value ? 1f : 0f;
    }
}
