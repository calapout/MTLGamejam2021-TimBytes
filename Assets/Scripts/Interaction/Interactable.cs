using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

[SerializeField]
[System.Serializable]
public class InteractionEvent : UnityEvent<InteractionEventCode> { }

[SerializeField]
[System.Serializable]
public enum InteractionEventCode { Default, Success, Failed }

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool interactable = true;
    public bool interactableOnce = false;
    public string interactionDescription = "";
    private bool interactedWith = false;
    public string dispatchToEventManager = "";

    public InteractionEvent OnInteraction;

    public virtual void Start()
    {
        SetInteractable(interactable);
    }

    public virtual void Interact(PlayerController originEntity, InteractionEventCode eventCode = InteractionEventCode.Default)
    {
        if (!interactedWith && interactableOnce)
        {
            interactedWith = true;
            SetInteractable(false);
        }
        OnInteraction?.Invoke(eventCode);
        if (dispatchToEventManager != "") { EventManager.Dispatch(dispatchToEventManager, null); }
    }

    public virtual string GetInteractionDescription()
    {
        return interactionDescription;
    }

    public void SetInteractable(bool val)
    {
        interactable = val;
        if (val) { InteractionManager.AddToInteractiveObjects(this); }
        else { InteractionManager.RemoveFromInteractiveObjects(this); }
    }

    public bool GetIsInteractable()
    {
        return interactable;
    }

}
