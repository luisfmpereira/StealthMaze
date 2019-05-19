using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField]
    Transform trans;
    [SerializeField]
    float rotSpeed = 25;

    void Start()
    {
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        trans.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }
}
