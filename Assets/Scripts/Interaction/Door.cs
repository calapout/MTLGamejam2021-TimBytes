using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bytes;

public class Door : Interactable
{

    public Transform targetToTeleport;
    public float rotation = 90f;
    public bool locked = false;
    public AudioSource source;

    public override void Start()
    {
        base.Start();

        source = GetComponent<AudioSource>();
    }

    public override void Interact(PlayerController originEntity, InteractionEventCode eventCode = InteractionEventCode.Default)
    {
        if (locked) { EventManager.Dispatch("playSound", new PlaySoundData("doorLocked")); return; }

        base.Interact(originEntity, eventCode);
        print("Open door!");
        TeleportPlayer();
    }

    public void UnlockDoor()
    {
        locked = false;
    }

    public override string GetInteractionDescription()
    {
        if (locked) { return "Door locked..."; }

        return base.GetInteractionDescription();
    }

    public void TeleportPlayer()
    {
        GameManager.instance.TeleportPlayerTo(targetToTeleport, rotation);
    }

}
