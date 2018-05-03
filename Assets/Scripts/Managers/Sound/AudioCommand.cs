using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public enum PlayMode { playSingle, PlayOneShot, Loop }

public enum Priority { Normal, High, HighWithSkip }

public class AudioCommand : MonoBehaviour
{

    public AudioClip Clip;
    public AudioSource source;
    public string CommandText;
    public AudioMixerGroup audioMixerGroup;

    public PlayMode PlayMode;
    public Priority Priority;
    private bool isPaused;

    // Use this for initialization
    void Start()
    {
        this.source.clip = Clip;
        //this.source.name = Clip.name;
    }

    private void Awake()
    {
        this.source = this.gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = audioMixerGroup;
        if (this.Priority == Priority.High)
        {
            this.source.priority = 1;
        }
        if (this.Priority == Priority.HighWithSkip)
        {
            this.source.priority = 1;
            this.source.bypassEffects = true;
            this.source.bypassListenerEffects = true;
            this.source.bypassReverbZones = true;
        }
    }

    public void Play()
    {
        switch (PlayMode)
        {
            case PlayMode.PlayOneShot:
                source.PlayOneShot(Clip);
                break;
            case PlayMode.playSingle:
                source.loop = false;
                if (!source.isPlaying)
                {
                    source.clip = Clip;
                    source.Play();
                }
                break;
            case PlayMode.Loop:
                if (this.isPaused)
                {
                    this.isPaused = false;
                    source.Play();
                }
                else
                {
                    source.Play();
                }

                break;
        }
    }

}
