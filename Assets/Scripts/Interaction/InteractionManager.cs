using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class InteractableEvent : UnityEvent<Interactable> { }
public class InteractionManager : MonoBehaviour
{
    #region Singleton
    static private InteractionManager instance;
    static private InteractionManager GetInstance()
    {
        return InteractionManager.instance;
    }
    #endregion

    public float interactThresholdDistance = 3f;

    private PlayerController player;
    private List<Interactable> interactablesObjects;

    public Interactable currentInteractableObject;

    public InteractableEvent OnInteractableObjectDetected;
    public InteractableEvent OnInteraction;

    private void Awake()
    {
        instance = this;

        player = GameObject.FindObjectOfType<PlayerController>();
        interactablesObjects = new List<Interactable>();

        OnInteractableObjectDetected = new InteractableEvent();
        OnInteraction = new InteractableEvent();
    }

    private void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            Vector3 playerPos = player.transform.position;
            int length = interactablesObjects.Count;

            Interactable closestObject = null;
            float closestDistance = float.MaxValue;

            // Find closest interactable object (If there is any)
            for (int i = 0; i < length; i++)
            {
                Interactable obj = interactablesObjects[i];
                if (!obj.GetIsInteractable() || obj == null) { continue; }

                Vector3 pos = obj.transform.position;
                float distance = Vector3.Distance(playerPos, pos);
                if (distance <= interactThresholdDistance)
                {
                    if (distance < closestDistance) { closestObject = obj; }
                }
            }

            if (currentInteractableObject != null && closestObject == null)
            { OnInteractableObjectDetected?.Invoke(closestObject); }
            else if (currentInteractableObject == null && closestObject != null)
            { OnInteractableObjectDetected?.Invoke(closestObject); }
            currentInteractableObject = closestObject;

        }

    }

    static public void InteractWithCurrentObject()
    {
        if (instance.currentInteractableObject != null)
        {
            instance.currentInteractableObject.Interact(instance.player);
            instance.OnInteraction?.Invoke(instance.currentInteractableObject);
        }
    }

    static public void AddToInteractiveObjects(Interactable interactableObject)
    {
        GetInstance().interactablesObjects.Add(interactableObject);
    }

    static public void RemoveFromInteractiveObjects(Interactable interactableObject)
    {
        GetInstance().interactablesObjects.Remove(interactableObject);
    }

}