using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Canette : MonoBehaviour
{

    private bool alreadyHit = false;

    public UnityEvent OnHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyHit) { return; }

        if (collision.transform.GetComponent<Couteau>() != null)
        {
            alreadyHit = true;
            OnHit?.Invoke();
        }
    }

}
