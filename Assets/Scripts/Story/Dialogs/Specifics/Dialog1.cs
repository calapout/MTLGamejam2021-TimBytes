using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class Dialog1 : Dialog
{

    public AnnaEntity annaEntity;

    public override void Start()
    {
        base.Start();
        HandleDialog(null);
    }

    public override void HandleDialog(Bytes.Data data)
    {
        if (alreadyPlayed) { return; }

        base.HandleDialog(data);
        print("Dialog 2!");
        PlayerController player = GameManager.instance.player;

        player.canBeControlled = false;

        EventManager.Dispatch("setDialogText", new DialogDataBytes("Come find me...", 1));
        annaEntity.PlayDialog(0, () => {
            player.canBeControlled = true;
            annaEntity.gameObject.SetActive(false);

            WaitForNextDialog(() => {
                EventManager.Dispatch("setDialogText", new DialogDataBytes("Anna... Anna! I have to find her again and get out of here...", 0));
                player.PlayDialog(0, () => {
                    EventManager.Dispatch("setDialogText", new DialogDataBytes("", 0));
                    
                });
            });

        });

    }

}
