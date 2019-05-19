using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    #region COLLECTIBLES
    [SerializeField]
    int maxCollectibles = 3;
    public int currentCollectibles;
    [SerializeField]
    Text collectibleCount;
    #endregion

    #region TIMERS
    [SerializeField]
    float maxTime = 120f;
    public Text timeText;
    #endregion


    void Awake()
    {
        Instance = this;
    }

    void Update()
    {

        maxTime -= Time.deltaTime;
        timeText.text = ((int)(maxTime + 1) + "s").ToString();
        collectibleCount.text = (currentCollectibles + "/" + maxCollectibles).ToString();

        if (maxTime <= 0)
        {
            //time over
            SceneManager.LoadScene(0);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && currentCollectibles == maxCollectibles)
        {
            SceneManager.LoadScene("Credits");
        }
    }


}
