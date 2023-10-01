using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public event Action OnDialogueComplete;

    public bool IsDialogueRunning = false;

    public void EndDialogue()
    {
        IsDialogueRunning = false;
        OnDialogueComplete?.Invoke();
    }
}
