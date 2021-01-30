using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bytes;

public class MiniGameCanettes : MiniGame
{

    public Canette[] canettes;
    public int progression;
    public int max = 3;

    private void Start()
    {
        for (int i = 0; i < canettes.Length; i++)
        {
            int copiedI = i;
            canettes[i].OnHit.AddListener(() => {
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

        // Start little Dialog
        if (progression == 1)
        {
            EventManager.Dispatch("startDialog2", null);
        }

        CheckIfDone();
    }

    private void CheckIfDone()
    {
        if (progression >= max && !alreadyDone) { Done(); }
    }

}
