using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class Dialog : MonoBehaviour
{

    public string eventListened = "startDialog1";
    private bool alreadyPlayed = false;

    public UnityEvent OnDialogStarted;

    public void Start()
    {
        EventManager.AddEventListener(eventListened, HandleDialog);
    }

    public virtual void HandleDialog(Bytes.Data data)
    {
        if (alreadyPlayed) { return; }

        alreadyPlayed = true;
        OnDialogStarted?.Invoke();
    }

}
