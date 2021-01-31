using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Bytes;

public class DialogPlayer : MonoBehaviour
{

    public AudioSource source;
    public AudioClip[] dialogs;

    public void Start()
    {

    }

    public void PlayDialog(int index, System.Action callback = null)
    {
        source.clip = dialogs[index];
        Animate.Delay(source.clip.length, callback);
        source.Play();
    }

}
