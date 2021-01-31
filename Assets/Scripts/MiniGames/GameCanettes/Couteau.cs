using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Couteau : MonoBehaviour
{

    private bool alreadyHit = false;
    public bool dontDestroy = false;
    public UnityEvent OnHitGround;

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyHit) { return; }

        if (collision.transform.tag == "Sol")
        {
            alreadyHit = true;
            Bytes.Animate.Delay(5f, ()=> {
                OnHitGround?.Invoke();
                if(!dontDestroy) Destroy(this.gameObject);
            });
        }
    }

}
