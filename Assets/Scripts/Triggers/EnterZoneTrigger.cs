using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Crystal.UI
{
    public class EnterZoneTrigger : BaseTrigger
    {

        // Don't confuse this trigger class from OnTriggerEnter in unity lol
        private void OnTriggerEnter(Collider other)
        {
            var possibleEntity = other.transform.GetComponent<PlayerController>();
            if (possibleEntity != null)
            {
                print(possibleEntity);
                // Entity Filters are inside base.Trigger()
                base.Trigger(possibleEntity.transform);
            }
        }

    }
}
