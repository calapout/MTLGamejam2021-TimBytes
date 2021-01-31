using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class Dialog5 : Dialog
{

    public AnnaEntity annaEntity;
    public Light endLight;

    public override void HandleDialog(Bytes.Data data)
    {
        if (alreadyPlayed) { return; }

        base.HandleDialog(data);
        print("Dialog 4 Cuisine!");
        PlayerController player = GameManager.instance.player;

        player.canBeControlled = false;

        EventManager.Dispatch("setDialogText", new DialogDataBytes("Wait... I don't have a sister?!", 0));

        float start = endLight.intensity;
        Animate.LerpSomething(4f, (f)=> {
            endLight.intensity = Mathf.Lerp(start, 0f, f);
        });

        player.PlayDialog(8, () => {

            EventManager.Dispatch("playEyeBlink", null);
            Animate.Delay(0.05f, () => {
                annaEntity.gameObject.SetActive(true);
            });

            WaitForNextDialog(() => {
                EventManager.Dispatch("setDialogText", new DialogDataBytes("", 1));
                annaEntity.PlayDialog(6, () => {
                    EventManager.Dispatch("setDialogText", new DialogDataBytes("", 0));
                    EventManager.Dispatch("startCredits", null);
                });
            });

        });
    }

}
