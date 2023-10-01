using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SlidingDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject m_SlidingDoor;

    [Header("Prompt")]
    [SerializeField] private TextMeshProUGUI m_PromptText;
    [SerializeField] private InGameUIContainer m_PromptContainer;
    [SerializeField] private string m_TextWhenOpen = "Close";
    [SerializeField] private string m_TextWhenClosed = "Open";
    private bool m_CanShowPrompt = true;

    [Header("Interaction")]
    [SerializeField] private bool IsOpen;
    private bool m_CanInteract = true;

    [Header("Animation")]
    [SerializeField] private float m_AnimationDuration = 1.2f;
    [SerializeField] private GameObject m_ClosedTransform;
    [SerializeField] private GameObject m_OpenTransform;
    [SerializeField] private AnimationCurve m_HorizontalCurve; // X
    [SerializeField] private AnimationCurve m_OffsetCurve; // Z

    [SerializeField] private PassThroughCollider m_PassThroughCollider;

    private void Start()
    {
        m_PromptText.text = IsOpen ? m_TextWhenOpen : m_TextWhenClosed;
    }

    public void Interact()
    {
        if(m_CanInteract)
        {
            SlideDoor();
        }
    }

    private void SlideDoor()
    {
        // Prevent from interacting while animation
        m_CanShowPrompt = false;
        m_CanInteract = false;

        ShowPrompt(false);

        if(IsOpen)
        {
            // close door
            m_PromptText.text = m_TextWhenClosed;

            StartCoroutine(SlideDoorCloseCo());
        }
        else
        {
            // open door

            m_PromptText.text = m_TextWhenOpen;

            StartCoroutine(SlideDoorOpenCo());

            m_PassThroughCollider.OnColliderExit += PassthroughCollider_OnColliderExit;
        }
    }

    public void ShowPrompt(bool value)
    {
        if(m_CanShowPrompt)
        {
            m_PromptContainer.Show(value);
        }
    }

    private void PassthroughCollider_OnColliderExit()
    {
        if(IsOpen)
        {
            ShowPrompt(false);
            m_PromptText.text = m_TextWhenClosed;

            StartCoroutine(SlideDoorCloseCo());
        }
    }

    private IEnumerator SlideDoorOpenCo()
    {
        float elapsedTime = 0;
        float animationRate = 0;

        float localX, localZ = 0f;

        while(elapsedTime < m_AnimationDuration)
        {
            yield return null;

            elapsedTime += Time.deltaTime;
            animationRate = elapsedTime / m_AnimationDuration;

            localX = Mathf.Lerp(m_ClosedTransform.transform.localPosition.x, m_OpenTransform.transform.localPosition.x, m_HorizontalCurve.Evaluate(animationRate));
            localZ = Mathf.Lerp(m_ClosedTransform.transform.localPosition.z, m_OpenTransform.transform.localPosition.z, m_HorizontalCurve.Evaluate(animationRate));

            m_SlidingDoor.transform.localPosition = new Vector3(localX, m_SlidingDoor.transform.localPosition.y, localZ);
        }

        m_CanShowPrompt = true;
        m_CanInteract = true;

        IsOpen = true;
    }

    private IEnumerator SlideDoorCloseCo()
    {
        float elapsedTime = 0;
        float animationRate = 0;

        float localX, localZ = 0f;

        while(elapsedTime < m_AnimationDuration)
        {
            yield return null;

            elapsedTime += Time.deltaTime;
            animationRate = elapsedTime / m_AnimationDuration;

            localX = Mathf.Lerp(m_ClosedTransform.transform.localPosition.x, m_OpenTransform.transform.localPosition.x, m_HorizontalCurve.Evaluate(1 - animationRate));
            localZ = Mathf.Lerp(m_ClosedTransform.transform.localPosition.z, m_OpenTransform.transform.localPosition.z, m_HorizontalCurve.Evaluate(1 - animationRate));

            m_SlidingDoor.transform.localPosition = new Vector3(localX, m_SlidingDoor.transform.localPosition.y, localZ);
        }

        m_CanShowPrompt = true;
        m_CanInteract = true;

        IsOpen = false;
    }
}
