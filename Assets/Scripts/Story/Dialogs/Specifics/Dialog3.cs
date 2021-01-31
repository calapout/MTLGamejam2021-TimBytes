using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class Dialog3 : Dialog
{

    public AnnaEntity annaEntity;

    public override void HandleDialog(Bytes.Data data)
    {
        if (alreadyPlayed) { return; }

        base.HandleDialog(data);
        print("Dialog 3!");
        PlayerController player = GameManager.instance.player;

        EventManager.Dispatch("playSound", new PlaySoundData("salon_whitenoise", 0.31f));

        EventManager.Dispatch("setDialogText", new DialogDataBytes("Come on Anna! It's not funny anymore!", 0));
        player.PlayDialog(4, ()=> {

            WaitForNextDialog(()=> {
                EventManager.Dispatch("setDialogText", new DialogDataBytes("You still have to catch her...", 1));
                annaEntity.PlayDialog(2, ()=> {

                    EventManager.Dispatch("playEyeBlink", null);
                    EventManager.Dispatch("setDialogText", new DialogDataBytes("", 0));
                    Animate.Delay(0.05f, ()=> {
                        annaEntity.gameObject.SetActive(false);
                    });
                    
                });
            });

        });
    }

}
