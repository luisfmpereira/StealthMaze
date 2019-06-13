﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;

public class CollectibleHandler : MonoBehaviour
{

    public AudioController audioControllerInstance;
    public AudioSource source;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
        audioControllerInstance = FindObjectOfType<AudioController>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioControllerInstance.PlayCollect(source);
            Instance.currentCollectibles++;
            Destroy(this.gameObject, 0.1f);
        }

    }

}
