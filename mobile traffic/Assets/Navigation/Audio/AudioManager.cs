using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.Volume;
            s.source.loop = s.loop;

        }
    }

    public void Play(string name)
    {
        Sound s=Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("no such clip");
            return;
        }
        s.source.Play();
    }
}
