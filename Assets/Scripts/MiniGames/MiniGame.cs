using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class MiniGame : MonoBehaviour
{

    public UnityEvent OnDone;
    protected bool alreadyDone = false;

    public virtual void Done()
    {
        if (alreadyDone) { return; }

        alreadyDone = true;
        OnDone?.Invoke();
    }
    
}
