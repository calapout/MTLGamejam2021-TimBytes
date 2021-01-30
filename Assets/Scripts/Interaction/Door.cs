using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{

    public Transform targetToTeleport;

    public override void Interact(PlayerController originEntity, InteractionEventCode eventCode = InteractionEventCode.Default)
    {
        base.Interact(originEntity, eventCode);

        TeleportPlayer();
    }

    public void TeleportPlayer()
    {
        GameManager.instance.TeleportPlayerTo(targetToTeleport);
    }

}
