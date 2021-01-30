using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceInSocket : MonoBehaviour
{
    public float translationSpeed = 1f;
    public float rotatioSpeed = 1f;

    public Transform targetObject;
    public Vector3 objFinalLocalPosition;
    public Vector3 objFinalRotation;
    public float distanceToTrigger = 3f;
    private bool isBeingPlaced = false;
    private bool doneCalled = false;

    public UnityEvent OnPlaced;

    private void Update()
    {
        Vector3 worldTranslation = transform.TransformPoint(objFinalLocalPosition);
        if (!isBeingPlaced)
        {
            if (Vector3.Distance(this.transform.position, targetObject.position) <= distanceToTrigger)
            {
                isBeingPlaced = true;
                var rg = targetObject.GetComponent<Rigidbody>();
                rg.useGravity = false;
                rg.velocity = Vector3.zero;
                rg.angularVelocity = Vector3.zero;
                targetObject.GetComponent<Collider>().isTrigger = true;
                targetObject.transform.tag = "Untagged";
                Bytes.EventManager.Dispatch("playerDropsObject", null);
            }
        }
        else if (Vector3.Distance(targetObject.position, worldTranslation) > 0.009f)
        {
            // Place object each frame
            targetObject.position = Vector3.Lerp(targetObject.position, worldTranslation, Time.deltaTime * translationSpeed);
            targetObject.rotation = Quaternion.Lerp(targetObject.rotation, Quaternion.Euler(objFinalRotation), Time.deltaTime * rotatioSpeed);
        }
        else if(!doneCalled)
        {
            doneCalled = true;
            targetObject.position = worldTranslation;
            targetObject.rotation = Quaternion.Euler(objFinalRotation);
            OnPlaced?.Invoke();
            Destroy(this);
        }
    }

}
