using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bytes;

public class MiniGameSockets : MiniGame
{

    public PlaceInSocket[] sockets;
    public int progression;
    public int max = 3;

    private void Start()
    {
        for (int i = 0; i < sockets.Length; i++)
        {
            int copiedI = i;
            sockets[i].OnPlaced.AddListener(() => {
                AddProgression();
            });
        }
    }

    public override void Done()
    {
        if (alreadyDone) { return; }

        base.Done();

        // Special code...
        Bytes.EventManager.Dispatch("playSound", new PlaySoundData("door_unlock", 0.5f));
    }

    public void AddProgression()
    {
        progression++;

        if(progression == 1) {

            EventManager.Dispatch("setDialogText", new DialogDataBytes("Maybe there is more...", 0));
            GameManager.instance.player.PlayDialog(3, ()=> {
                EventManager.Dispatch("setDialogText", new DialogDataBytes("", 0));
            });
        }

        CheckIfDone();
    }

    private void CheckIfDone()
    {
        if (progression >= max && !alreadyDone) { Done(); }
    }

}
