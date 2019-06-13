using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField]
    AudioClip gunshot;
    [SerializeField]
    AudioClip collect;

    static AudioController audioControllerInstance;
    void Awake() => audioControllerInstance = this;

    public void PlayGunShot(AudioSource source) => source.PlayOneShot(gunshot, 1f);
    public void PlayCollect(AudioSource source) => source.PlayOneShot(collect, 1f);

}
