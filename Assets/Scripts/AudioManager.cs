using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource popSource, mergeSource;
    [SerializeField] private AudioClip pop, merge;

    public void PlayPop(int amount)
    {
        popSource.pitch = 1 + amount * 0.1f;
        popSource.Play();
    }

    public void PlayMerge()
    {
        mergeSource.Play();
    }
}
