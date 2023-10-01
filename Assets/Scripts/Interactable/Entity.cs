using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class Entity : MonoBehaviour, IInteractable
{
    private DialogueRunner m_DialogueRunner;

    [Header("Prompt")]
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private string m_TextWhenHovering = "Talk";
    [SerializeField] private InGameUIContainer m_PromptContainer;

    [Header("Dialogue")]
    [SerializeField] private InGameUIContainer m_DialogueContainer;
    [SerializeField] private DialogueViewBase m_DialogueView;
    [SerializeField] private string m_StartNode;

    private void Start()
    {
        m_DialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    public void Interact()
    {
        // If using bubbles : set the entity's bubble as Dialogue View to Dialogue Runner
        DialogueViewBase[] dialogueViewList = { m_DialogueView};
        m_DialogueRunner.SetDialogueViews(dialogueViewList);

        m_DialogueContainer.Show(true);

        // Hide prompt

        // Move Camera ?

        // Start dialogue
        m_DialogueRunner.StartDialogue(m_StartNode);
    }

    public void ShowPrompt(bool value)
    {
        Debug.Log("Show prompt " + value);
        m_PromptContainer.Show(value);
    }
}
