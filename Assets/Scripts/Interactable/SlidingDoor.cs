using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SlidingDoor : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private TextMeshProUGUI m_PromptText;
    [SerializeField] private InGameUIContainer m_PromptContainer;
    [SerializeField] private string m_TextWhenOpen = "Close";
    [SerializeField] private string m_TextWhenClosed = "Open";
    private bool m_CanShowPrompt = true;

    [Header("Interaction")]
    [SerializeField] private bool IsOpen;

    public void Interact()
    {
        SlideDoor();
    }

    private void SlideDoor()
    {
        if(IsOpen)
        {
            // close door

            m_PromptText.text = m_TextWhenClosed;
        }
        else
        {
            // open door

            m_PromptText.text = m_TextWhenOpen;
        }

        // While animation, prevent from showing prompt (and hide it)
    }

    public void ShowPrompt(bool value)
    {
        if(m_CanShowPrompt)
        {
            m_PromptContainer.Show(value);
        }
    }
}
