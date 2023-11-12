using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sound Effect")]
    public AudioSource[] soundEffects;
    public AudioSource shootSound;
    public AudioSource[] reloadSoundEffect;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySoundEffect(int soundToPlay)
    {
        if (!soundEffects[soundToPlay].isPlaying) 
            //再播放
            soundEffects[soundToPlay].Play();
    }

    public void PlayShootSound()
    {
        shootSound.Play();
    }

    public void StopSoundEffect(int soundToPlay)
    {
        if (soundEffects[soundToPlay].isPlaying)
            //再播放
            soundEffects[soundToPlay].Stop();
    }

    public void PauseSoundEffect()
    {
        for(int i = 0; i < soundEffects.Length; i++)
        {
            if (soundEffects[i].isPlaying)
                //再播放
                soundEffects[i].Pause();
        }
    }

    public void PlayReloadSoundEffect(int state)
    {
        for (int i = 0; i < reloadSoundEffect.Length; i++)
        {
            reloadSoundEffect[i].Stop();
        }
        reloadSoundEffect[state].Play();
    }
}
