using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static PlayerMovement;

public class PlayerHealth : MonoBehaviour
{
    #region Health
    public float healthPoints = 3;
    private float healthMax;
    public Image healthBar;
    #endregion

    #region Stealth

    public float stealthPoints = 100f;
    public float maxStealth;
    public float runStealthHit = 10f;
    public float camStealthHit = 20f;
    private bool takingDamage = false;

    public Image stealthBar;
    #endregion

    void Start()
    {
        healthMax = healthPoints;
        maxStealth = stealthPoints;
    }
    void Update()
    {
        //healthbar fill
        stealthBar.fillAmount = stealthPoints / maxStealth;
        healthBar.fillAmount = healthPoints / healthMax;

        //recover damage
        if (!takingDamage && stealthPoints <= maxStealth)
            stealthPoints += runStealthHit * Time.deltaTime;


        //take running damage
        if (playerMovement.running)
            TakeStealthDamage(runStealthHit);
        else
            StopStealthDamage();


        //die
        if (healthPoints <= 0 || stealthPoints <= 0)
            SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            healthPoints--;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "StealthDamage")
            TakeStealthDamage(camStealthHit);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "StealthDamage")
            StopStealthDamage();
    }
    public void TakeStealthDamage(float stealthHit)
    {
        stealthPoints -= stealthHit * Time.deltaTime;
        takingDamage = true;
    }
    public void StopStealthDamage()
    {
        takingDamage = false;
    }
}
