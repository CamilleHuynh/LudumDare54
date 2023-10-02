using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class Entity : MonoBehaviour, IInteractable
{
    private DialogueRunner m_DialogueRunner;
    private DialogueManager m_DialogueManager;

    [Header("Prompt")]
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private string m_TextWhenHovering = "Talk";
    [SerializeField] private InGameUIContainer m_PromptContainer;

    [Header("Dialogue")]
    [SerializeField] private InGameUIContainer m_DialogueContainer;
    [SerializeField] private DialogueViewBase m_DialogueView;
    [SerializeField] private string m_StartNode;

    private bool m_CanShowPrompt = true;
    private bool m_HasAlreadyTalked = false;

    private void Start()
    {
        m_DialogueRunner = FindObjectOfType<DialogueRunner>();
        m_DialogueManager = FindObjectOfType<DialogueManager>();

        m_CanShowPrompt = true;
        m_HasAlreadyTalked = true;
    }

    public void Interact()
    {
        if(!m_DialogueManager.IsDialogueRunning && !m_HasAlreadyTalked)
        {
            // If using bubbles : set the entity's bubble as Dialogue View to Dialogue Runner
            DialogueViewBase[] dialogueViewList = { m_DialogueView};
            m_DialogueRunner.SetDialogueViews(dialogueViewList);

            m_DialogueContainer.Show(true);

            m_DialogueManager.IsDialogueRunning = true;
            m_DialogueManager.OnDialogueComplete += DialogueManager_OnDialogueComplete;

            // Hide prompt
            m_PromptContainer.Show(false);
            m_CanShowPrompt = false;

            // Move Camera ?

            // Start dialogue
            m_DialogueRunner.StartDialogue(m_StartNode);

            m_HasAlreadyTalked = true;
        }
    }

    public void ShowPrompt(bool value)
    {
        if(!m_DialogueManager.IsDialogueRunning && !m_HasAlreadyTalked)
        {
            m_PromptContainer.Show(m_CanShowPrompt && value);
        }
    }

    private void DialogueManager_OnDialogueComplete()
    {
        m_DialogueManager.OnDialogueComplete -= DialogueManager_OnDialogueComplete;

        // Hide dialogue view
        m_DialogueContainer.Show(false);

        m_CanShowPrompt = true;
    }
}
