using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{


    public Transform muzzle;
    public Rigidbody bulletPrefab;
    public float bulletInitialForce = 10;
    public float cooldown;
    float maxCooldown;

    void Update()
    {
        if (cooldown <= 0 && Input.GetMouseButtonDown(0))
            Shoot();
        else
            cooldown -= Time.deltaTime;

    }



    void Shoot()
    {
        Rigidbody b = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as Rigidbody;
        b.AddForce(muzzle.forward * bulletInitialForce);
    }


}
