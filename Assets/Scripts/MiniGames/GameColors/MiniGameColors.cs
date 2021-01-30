using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameColors : MiniGame
{

    public CodeGameNumber[] numbers;
    public int[] currentCombination = new int[4];
    public int[] goodCombination;

    private void Start()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            int copiedI = i;
            numbers[i].OnValueChange.AddListener((int newNumber)=> {
                SetNumberAtIndex(copiedI, newNumber);
            });
        }
    }

    public override void Done()
    {
        if (alreadyDone) { return; }

        base.Done();

        // Special code...

    }

    public void SetNumberAtIndex(int index, int value)
    {
        currentCombination[index] = value;
        CheckIfDone();
    }

    private void CheckIfDone()
    {
        for (int i = 0; i < currentCombination.Length; i++)
        {
            if (currentCombination[i] != goodCombination[i]) { return; }
        }
        Done();
    }

}
