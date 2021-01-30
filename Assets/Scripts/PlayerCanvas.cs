using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Bytes;

public class PlayerCanvas : MonoBehaviour
{
    public Text PlayerSpeech;
    public Animator animFondue;

    private void Start()
    {
        EventManager.AddEventListener("setInteractableText", (Bytes.Data d) => {
            if (d == null) { UpdateSpeechText(null); return; }
            UpdateSpeechText(((StringDataBytes)d).StringValue);
        });
    }

    public void UpdateSpeechText(string newSpeechValue = null)
    {
        if (newSpeechValue is null) newSpeechValue = "";
        PlayerSpeech.text = newSpeechValue;
    }

    public void PlayFondue(System.Action callback)
    {
        Animate.Delay(0.8f, callback);
        Utils.PlayAnimatorClip(animFondue, "PlayFondue", null);
    }

}
