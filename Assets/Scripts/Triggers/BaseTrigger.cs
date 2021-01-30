using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseTrigger : MonoBehaviour
{
    public bool triggerOnlyOnce;
    private bool hasTriggered;

    public UnityEvent OnTrigger;

    public void Trigger(Transform entity)
    {
        if (triggerOnlyOnce && hasTriggered) { Destroy(this.gameObject); return; }

        hasTriggered = true;
        TriggerEffect(entity);
    }

    public virtual void TriggerEffect(Transform entity)
    {
        OnTrigger?.Invoke();
    }

}