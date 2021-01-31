using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class Dialog : MonoBehaviour
{
    static public readonly float TIME_BETWEEN_DIALOGS = 0.5f;

    public string eventListened = "startDialog1";
    protected bool alreadyPlayed = false;

    public UnityEvent OnDialogStarted;

    public void Start()
    {
        EventManager.AddEventListener(eventListened, HandleDialog);
    }

    public void StartDialog()
    {
        HandleDialog(null);
    }

    public virtual void HandleDialog(Bytes.Data data)
    {
        if (alreadyPlayed) { return; }

        alreadyPlayed = true;
        OnDialogStarted?.Invoke();
    }

    public void WaitForNextDialog(System.Action callback, float time = -1f)
    {
        if (time == -1f) { time = TIME_BETWEEN_DIALOGS; }
        Animate.Delay(time, callback);
    }

}
