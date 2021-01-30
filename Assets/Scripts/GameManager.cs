using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerCanvas playerCanvas;
    public InteractionManager interactionManager;

    private void Start()
    {
        interactionManager.OnInteractableObjectDetected.AddListener((Interactable InteractableObject) => {
            playerCanvas.UpdateSpeechText(InteractableObject?.GetInteractionDescription());
        });
        interactionManager.OnInteraction.AddListener((Interactable InteractableObject) => {
            playerCanvas.UpdateSpeechText(null);
        });
    }

}
