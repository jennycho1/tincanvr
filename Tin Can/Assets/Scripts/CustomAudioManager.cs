using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CustomAudioManager : MonoBehaviour
{
    public TinCanSound[] sounds;

    //public static PlaybookAudioManager instance;

    void Awake()
    {
        /*
        // persistent across levels
        if (instance == null){
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        */
        foreach (TinCanSound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }
    // background music
    void Start()
    {
        //Play("backgroundmusicname");
        Play("background");
    }

    // Play sound method
    public void Play(string name)
    {   
        // find a right sound source from the array. using System;
        TinCanSound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning("Sound"+name+" not found!");
            return;
        }

        // FindObjectOfType<CustomAudioManager>().Play("nameofsound");

        s.source.Play();
    }
}
