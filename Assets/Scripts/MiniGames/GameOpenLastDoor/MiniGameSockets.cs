using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    public void AddProgression()
    {
        progression++;
        CheckIfDone();
    }

    private void CheckIfDone()
    {
        if (progression >= max && !alreadyDone) { Done(); }
    }

}
