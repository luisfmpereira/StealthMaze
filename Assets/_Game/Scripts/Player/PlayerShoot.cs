using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{


    public Transform muzzle;
    public Rigidbody bulletPrefab;
    public float bulletInitialForce = 250;
    public float cooldown;
    public float maxCooldown = 3f;
    public Image reloadBar;
    public Text reloadText;

    void Update()
    {

        reloadBar.fillAmount = Mathf.Clamp(1 - (cooldown / maxCooldown), 0, 1);


        if (cooldown <= 0 && Input.GetMouseButtonDown(0))
            Shoot();
        else
            cooldown -= Time.deltaTime;

        if (cooldown > 0)
            reloadText.text = "Reloading...";
        else
            reloadText.text = "Ready";


    }



    void Shoot()
    {
        Rigidbody b = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as Rigidbody;
        b.AddForce(muzzle.forward * bulletInitialForce);
        cooldown = maxCooldown;
    }


}
