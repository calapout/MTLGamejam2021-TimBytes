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
        player = GameObject.FindObjectOfType<PlayerController>();

        
    }

    private void Start()
    {
        interactionManager.OnInteractableObjectDetected.AddListener((Interactable InteractableObject) => {
            playerCanvas.UpdateSpeechText(InteractableObject?.GetInteractionDescription());
        });
        interactionManager.OnInteraction.AddListener((Interactable InteractableObject) => {
            playerCanvas.UpdateSpeechText(null);
            Animate.Delay(1f, () => {
                // Resets the tooltip
                interactionManager.currentInteractableObject = null;
            });
        });
    }

    public void UnlockDoor()
    {

    }

    public void TeleportPlayerTo(Transform target, float rotation)
    {
        player.canBeControlled = false;
        player.Stop();

        Rigidbody pickedItem = player.pickedItem;

        // Play black fondue
        playerCanvas.PlayFondue(()=> 
        {
            player.transform.position = target.position;
            player.transform.rotation = Quaternion.Euler(0, rotation, 0);
            // 

            if (pickedItem != null)
            {
                pickedItem.transform.position = player.pickedItemTarget.position;
                player.PickObject(pickedItem);
            }

            player.canBeControlled = true;
        });
    }

}
