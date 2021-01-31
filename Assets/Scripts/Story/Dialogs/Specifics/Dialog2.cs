using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class DialogDataBytes : Bytes.Data
{
    public DialogDataBytes(string stringValue, int color) { StringValue = stringValue; this.Color = color; }
    public string StringValue { get; private set; }
    public int Color { get; private set; }
}

public class Dialog2 : Dialog
{

    public AnnaEntity annaEntity;

    public override void HandleDialog(Bytes.Data data)
    {
        if (alreadyPlayed) { return; }

        base.HandleDialog(data);
        print("Dialog 2!");
        PlayerController player = GameManager.instance.player;

        EventManager.Dispatch("setDialogText", new DialogDataBytes("Anna! We have to leave that house now!", 0));
        player.PlayDialog(1, ()=> {
            WaitForNextDialog(()=> {
                EventManager.Dispatch("setDialogText", new DialogDataBytes("You have to catch me first...", 1));
                annaEntity.PlayDialog(1, ()=> {

                    EventManager.Dispatch("playEyeBlink", null);
                    EventManager.Dispatch("setDialogText", new DialogDataBytes("", 0));
                    Animate.Delay(0.05f, () => {
                        annaEntity.gameObject.SetActive(false);
                    });

                    WaitForNextDialog(()=> {
                        EventManager.Dispatch("setDialogText", new DialogDataBytes("Anna, come back!", 0));
                        player.PlayDialog(2, ()=> {
                            EventManager.Dispatch("setDialogText", new DialogDataBytes("", 0));
                        });
                    });

                });
            });

        });
    }

}
