using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurretController : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public Transform muzzle;
    public float bulletInitialForce = 250;
    public MeshRenderer mr;
    public float rotSpeed = 10;
    public Color[] combatStageColors;
    public float[] combatStages;
    private int currentCombatStage;
    private bool targetInRange;
    private bool canAttack;
    private float timer;
    private float timeToChange = 1.5f;
    private Transform trans;

    private Transform target;

    void Start()
    {
        targetInRange = false;
        canAttack = false;
        timer = 0;
        trans = GetComponent<Transform>();
        currentCombatStage = 0;
    }

    void Update()
    {
        mr.material.color = combatStageColors[currentCombatStage];

        if (!targetInRange)
        {
            trans.Rotate(0, rotSpeed * Time.deltaTime, 0);
        }
        else
        {
            trans.LookAt(target.position);
            trans.eulerAngles = new Vector3(0, trans.eulerAngles.y, 0);

            if (!canAttack)
            {
                timer += Time.deltaTime;
                if (timer >= timeToChange)
                {
                    currentCombatStage = 2;
                    timer = 0;
                    canAttack = true;
                    return;
                }
            }

            else
            {
                muzzle.LookAt(target.position);
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    timer = 0;
                    Rigidbody b = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as Rigidbody;
                    b.AddForce(muzzle.forward * bulletInitialForce);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            targetInRange = true;
            target = hit.transform;
            canAttack = false;
            timer = 0;
            currentCombatStage = 1;

        }
    }

    public void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            currentCombatStage = 0;
            targetInRange = false;
            target = null;
        }
    }




}
