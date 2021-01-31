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
    public Animator animEyeBlink;

    public Image hand;
    public Sprite handGrab;
    public Sprite handTouch;

    public Text dialogText;
    public Color[] dialogColors;

    private void Start()
    {
        EventManager.AddEventListener("setInteractableText", (Bytes.Data d) => {
            if (d == null) { UpdateSpeechText(null); return; }
            UpdateSpeechText(((StringDataBytes)d).StringValue);
        });
        EventManager.AddEventListener("playEyeBlink", (Bytes.Data d)=> {
            animEyeBlink.Play("EyeBlink", -1, 0);
        });
        EventManager.AddEventListener("setDialogText", (Bytes.Data d) => {
            dialogText.text = ((DialogDataBytes)d).StringValue;
            dialogText.color = dialogColors[((DialogDataBytes)d).Color];
        });
    }

    public void UpdateSpeechText(string newSpeechValue = null)
    {
        if (newSpeechValue is null) { newSpeechValue = ""; hand.GetComponent<CanvasGroup>().alpha = 0; }
        if (newSpeechValue == "&HAND_GRAB")
        {
            PlayerSpeech.text = "";
            hand.GetComponent<CanvasGroup>().alpha = 1f;
            hand.sprite = handGrab;
        }
        else if (newSpeechValue == "&HAND_TOUCH")
        {
            PlayerSpeech.text = "";
            hand.GetComponent<CanvasGroup>().alpha = 1f;
            hand.sprite = handTouch;
        }
        else
        {
            PlayerSpeech.text = newSpeechValue;
        }
    }

    public void PlayFondue(System.Action callback)
    {
        Animate.Delay(0.8f, callback);
        Utils.PlayAnimatorClip(animFondue, "PlayFondue", null);
    }

}
