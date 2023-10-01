using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private float m_MaxDistanceToInteract = 3f;
    [SerializeField] private LayerMask m_InteractableLayer;
    private FPSInputAction m_InputActions;
    private InputAction m_InteractAction;

    private bool m_CanInteract = true;
    private IInteractable m_LastInteractable;

    private void OnEnable()
    {
        m_InputActions = new FPSInputAction();

        // Fetch action
        m_InteractAction = m_InputActions.Base.Interact;
        m_InteractAction.performed += OnInteract;
        m_InteractAction.Enable();
    }

    private void Update()
    {
        if(m_CanInteract)
        {
            Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
            Debug.DrawLine(m_Camera.transform.position, m_Camera.transform.position + m_Camera.transform.forward * m_MaxDistanceToInteract, Color.blue, Time.deltaTime);

            RaycastHit[] hits = Physics.RaycastAll(ray, m_MaxDistanceToInteract, m_InteractableLayer);

            // If we are looking at an interactable
            int nbHits = hits.Length;
            if(nbHits > 0)
            {
                RaycastHit closestHit = hits[0];
                float hitDistance = 99999f;

                for(int i = 0; i < nbHits; i++)
                {
                    if(hits[i].distance < hitDistance)
                    {
                        closestHit = hits[i];
                        hitDistance = hits[i].distance;
                    }
                }

                GameObject target = closestHit.collider.gameObject;

                if(target.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    Debug.Log("Found interactible");

                    if(m_LastInteractable == null || interactable != m_LastInteractable)
                    {
                        Debug.Log("changed interactable");

                        if(m_LastInteractable != null)
                        {
                            Debug.Log("hide old prompt");
                            // Hide prompt of last interactable
                            m_LastInteractable.ShowPrompt(false);
                        }
                    }

                    interactable.ShowPrompt(true);

                    m_LastInteractable = interactable;
                }
            }
            else
            {
                if(m_LastInteractable != null)
                {
                    // Hide prompt of last interactable
                    m_LastInteractable.ShowPrompt(false);
                }
            }
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if(m_CanInteract)
        {
            Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, m_MaxDistanceToInteract, m_InteractableLayer);

            int nbHits = hits.Length;
            if(nbHits > 0)
            {
                RaycastHit closestHit = hits[0];
                float hitDistance = 99999f;

                for(int i = 0; i < nbHits; i++)
                {
                    if(hits[i].distance < hitDistance)
                    {
                        closestHit = hits[i];
                        hitDistance = hits[i].distance;
                    }
                }

                GameObject target = closestHit.collider.gameObject;

                if(target.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    m_LastInteractable = interactable;

                    // Start interact
                }
            }
        }
    }
}
