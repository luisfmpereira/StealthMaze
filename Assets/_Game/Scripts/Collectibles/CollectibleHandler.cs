using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;

public class CollectibleHandler : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instance.currentCollectibles++;
            Destroy(this.gameObject);
        }

    }

}
