using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStunned : MonoBehaviour
{

    public TurretController mainScript;

    void Awake()
    {
        mainScript = GetComponentInChildren<TurretController>();
    }

    public void OnTriggerEnter(Collider hit)
    {

        if (hit.gameObject.CompareTag("PlayerBullet"))
            mainScript.Stunned();
    }
}
