using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamStunned : MonoBehaviour
{

    SecurityCameraController mainScript;

    void Awake()
    {
        mainScript = GetComponentInChildren<SecurityCameraController>();
    }

    public void OnTriggerEnter(Collider hit)
    {

        if (hit.gameObject.CompareTag("PlayerBullet"))
            mainScript.Stunned();
    }
}
