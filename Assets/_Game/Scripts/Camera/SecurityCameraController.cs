using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraController : MonoBehaviour
{
    public Light spotLight;
    public float allertPeriod = 4;  
    public float rotSpeed = 5;
    private float initialAngle;
    private float finalAngle;
    public int sign = -1;
    private float angle;
    private Transform t; 
    private Transform target;

    private float timer;

    private bool hasSpottedTarget;
    private bool targetCaught;


    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        angle = t.localEulerAngles.y;
        initialAngle = angle;
        finalAngle = initialAngle + Mathf.Sign(initialAngle) * 90;
        timer = 0;
        hasSpottedTarget = false;
        targetCaught = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpottedTarget)
        {
            angle += sign * rotSpeed * Time.deltaTime;
            t.Rotate(0, sign * rotSpeed * Time.deltaTime, 0);
            if (angle >= finalAngle || angle <= initialAngle)
            {
                sign *= -1;
            }
        }
        else
        {
            if (!targetCaught)
            {
                timer += Time.deltaTime;
                if (timer >= allertPeriod)
                {
                    targetCaught = true;
                    timer = 0;
                    spotLight.color = Color.red;
                }
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= 2)
                {
    
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    
                }
            }            
        }
    }



    public void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            hasSpottedTarget = true;
            timer = 0;
            spotLight.color = Color.yellow;
        }
    }

    public void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            hasSpottedTarget = false;
            targetCaught = false;
            spotLight.color = Color.white;
        }
    }


}
