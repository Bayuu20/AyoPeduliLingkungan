using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSfx : MonoBehaviour
{
    public AudioSource audioSourceSfx;

    public AudioClip audioClipBenar;
    public AudioClip audioClipSalah;
    public AudioClip audioClipButton;

    public AudioClip audioClipFinish;
    public AudioClip audioClipFinish2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundSfxBenar()
    {
        audioSourceSfx.PlayOneShot(audioClipBenar); //Settingan volume itu 0.1 0.2 0.3
    }

    public void SoundSfxSalah()
    {
        audioSourceSfx.PlayOneShot(audioClipSalah);
    }

    public void soundFinish()
    {
        audioSourceSfx.PlayOneShot(audioClipFinish);
    }
    public void soundFinish2()
    {
        audioSourceSfx.PlayOneShot(audioClipFinish2);
    }

    public void soundButton()
    {
        audioSourceSfx.PlayOneShot(audioClipButton);
    }
}
