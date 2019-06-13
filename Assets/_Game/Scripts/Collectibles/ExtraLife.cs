using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{

    public PlayerHealth health;
    public AudioController audioControllerInstance;
    public AudioSource source;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
        audioControllerInstance = FindObjectOfType<AudioController>();
        health = FindObjectOfType<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioControllerInstance.PlayCollect(source);
            health.healthPoints++;
            Destroy(this.gameObject, 0.1f);
        }
    }
}
