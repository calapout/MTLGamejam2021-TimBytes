using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CouteauSpawner : MonoBehaviour
{

    public GameObject couteauPrefab;

    private void Start()
    {
        SpawnCouteau();
    }

    public void SpawnCouteau()
    {
        var g = GameObject.Instantiate(couteauPrefab, this.transform.position, Quaternion.Euler(0, -90, -90));
        var s = g.GetComponent<Couteau>();
        s.OnHitGround.AddListener(() => {
            SpawnCouteau();
            s.OnHitGround.RemoveAllListeners();
        });
    }

}
