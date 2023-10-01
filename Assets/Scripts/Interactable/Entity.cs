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

    private void Start()
    {
        m_DialogueRunner = FindObjectOfType<DialogueRunner>();
        m_DialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Interact()
    {
        // If using bubbles : set the entity's bubble as Dialogue View to Dialogue Runner
        DialogueViewBase[] dialogueViewList = { m_DialogueView};
        m_DialogueRunner.SetDialogueViews(dialogueViewList);

        m_DialogueContainer.Show(true);

        m_DialogueManager.OnDialogueComplete += DialogueManager_OnDialogueComplete;

        // Hide prompt
        ShowPrompt(false);
        m_CanShowPrompt = false;

        // Move Camera ?

        // Start dialogue
        m_DialogueRunner.StartDialogue(m_StartNode);
    }

    public void ShowPrompt(bool value)
    {
        m_PromptContainer.Show(m_CanShowPrompt && value);
    }

    private void DialogueManager_OnDialogueComplete()
    {
        m_DialogueManager.OnDialogueComplete -= DialogueManager_OnDialogueComplete;

        // Hide dialogue view
        m_DialogueContainer.Show(false);

        m_CanShowPrompt = true;
    }
}
