using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Bytes;

public class Credits : MonoBehaviour
{

    public Animator animCredits;

    private void Start()
    {
        EventManager.AddEventListener("startCredits", (Bytes.Data d)=> {

            Utils.PlayAnimatorClip(animCredits, "PlayCredits", ()=> {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });

        });
    }

}
