using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour, IInteractable
{
    [SerializeField] private InGameUIContainer m_PromptContainer;

    private bool m_CanShowPrompt = true;

    public void Interact()
    {

    }

    public void ShowPrompt(bool value)
    {

        m_PromptContainer.Show(m_CanShowPrompt && value);

    }
}
