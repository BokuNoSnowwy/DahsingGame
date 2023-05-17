using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] A_Sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in A_Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(A_Sounds, s => s.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " doesn't exist");
            return;
        }
        s.source.Play();
    }
}
