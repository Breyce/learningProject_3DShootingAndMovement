using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sound Effect")]
    public AudioSource[] soundEffects;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlaySoundEffect(int soundToPlay)
    {
        if (!soundEffects[soundToPlay].isPlaying) 
            //再播放
            soundEffects[soundToPlay].Play();
    }

    public void StopSoundEffect(int soundToPlay)
    {
        if (soundEffects[soundToPlay].isPlaying)
            //再播放
            soundEffects[soundToPlay].Stop();
    }

    public void PauseSoundEffect(int soundToPlay)
    {
        if (soundEffects[soundToPlay].isPlaying)
            //再播放
            soundEffects[soundToPlay].Pause();
    }
}
