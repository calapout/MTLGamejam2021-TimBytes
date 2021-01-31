﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class Dialog4_Cuisine : Dialog
{

    public AnnaEntity annaEntity;

    public override void HandleDialog(Bytes.Data data)
    {
        if (alreadyPlayed) { return; }

        base.HandleDialog(data);
        print("Dialog 4 Cuisine!");
        PlayerController player = GameManager.instance.player;

        EventManager.Dispatch("setDialogText", new DialogDataBytes("She wants you to kill some cans... HI HI HI", 1));
        annaEntity.PlayDialog(5, () => {

            EventManager.Dispatch("playEyeBlink", null);
            EventManager.Dispatch("setDialogText", new DialogDataBytes("", 0));
            Animate.Delay(0.05f, () => {
                annaEntity.gameObject.SetActive(false);
            });

            WaitForNextDialog(() => {
                EventManager.Dispatch("setDialogText", new DialogDataBytes("I guess I have to throw some knives...", 0));
                player.PlayDialog(6, () => {

                    EventManager.Dispatch("setDialogText", new DialogDataBytes("", 0));

                });
            });

        });
    }

}
