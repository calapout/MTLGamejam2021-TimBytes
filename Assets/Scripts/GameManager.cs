using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bytes;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public PlayerController player;

    public PlayerCanvas playerCanvas;
    public InteractionManager interactionManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();

        interactionManager.OnInteractableObjectDetected.AddListener((Interactable InteractableObject) => {
            playerCanvas.UpdateSpeechText(InteractableObject?.GetInteractionDescription());
        });
        interactionManager.OnInteraction.AddListener((Interactable InteractableObject) => {
            playerCanvas.UpdateSpeechText(null);
        });
    }

    public void TeleportPlayerTo(Transform target)
    {
        player.canBeControlled = false;
        player.Stop();

        // Play black fondue
        playerCanvas.PlayFondue(()=> 
        {
            player.transform.position = target.position;
            player.canBeControlled = true;
        });
    }

}
