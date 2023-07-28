using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] AudioSource audioSource;

    [SerializeField] float timeToSwitch;
    [SerializeField] AudioClip PlayOnStart;



    private void Start()
    {
        Play(PlayOnStart, true);
    }


    public void Play(AudioClip musicToPlay, bool interupt = false)
    {
        if (musicToPlay == null) { return; }
        if (interupt == true)
        {
            audioSource.volume = 1f;
            audioSource.clip = musicToPlay;
            audioSource.Play();

        }
        else
        {
            SwitchTo = musicToPlay;
            StartCoroutine(SmoothSwitchSounde());
        }

    }
    AudioClip SwitchTo;
    float Volume = 1f;

    IEnumerator SmoothSwitchSounde()
    {
        Volume = 1f;
        while (Volume > 0f)
        {
            Volume -= Time.deltaTime / timeToSwitch;
            if (Volume < 0f) { Volume = 0f; }
            audioSource.volume = Volume;
            yield return new WaitForEndOfFrame();
        }
        Play(SwitchTo, true);
    }

}
