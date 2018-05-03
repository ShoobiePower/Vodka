using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public float ammountTochangeBy;
    public AudioSource gameMusicSource;
    public AudioSource ambianceSource;
    public AudioClip barMusicLayered;
    public AudioClip barMusicStatic;
    public AudioClip menuMusicStatic;
    public enum MusicStates { INBAR, INBARGAIN, INMENU, INMENUGAIN, NONE };
    MusicStates currentMusicState;
    private float maxAmbiance;

    [SerializeField]
    private float maxMusicVolume;

    public void init()
    {
        maxAmbiance = ambianceSource.volume;
    }

    // Update is called once per frame
    void Update()
    {


        if (currentMusicState == MusicStates.INBAR)
        {
            runMapManagerFadeOut();
            startPatronSounds();

        }

        if (currentMusicState == MusicStates.INBARGAIN)
        {
            runFadeIn();

        }

        if (currentMusicState == MusicStates.INMENU)
        {
            runInBarFadeOut();
            exitPatronSound();
        }

    }


    public void runFadeIn()
    {
        gameMusicSource.volume += ammountTochangeBy * Time.deltaTime;
        if (gameMusicSource.volume >= .5 && .6f >= gameMusicSource.volume)  // for saftety 
        {
            currentMusicState = MusicStates.NONE;
        }
    }

    public void runInBarFadeOut()
    {
        gameMusicSource.volume -= ammountTochangeBy * Time.deltaTime;
        if (0 >= gameMusicSource.volume)
        {
            gameMusicSource.Stop();
            gameMusicSource.clip = menuMusicStatic;
            gameMusicSource.Play();
            changeMusicState(MusicStates.INBARGAIN);
        }
    }


    public void runMapManagerFadeOut()
    {
        gameMusicSource.volume -= ammountTochangeBy * Time.deltaTime;
        if (0 >= gameMusicSource.volume)
        {
            gameMusicSource.Stop();
            gameMusicSource.clip = barMusicLayered;
            Invoke("startBarStaticTheme", barMusicLayered.length);
            gameMusicSource.Play();
            changeMusicState(MusicStates.INBARGAIN);
        }
    }

    public void changeMusicState(MusicStates stateToChangeTo)
    {
        currentMusicState = stateToChangeTo;
    }

    public void startPatronSounds()
    {
        ambianceSource.volume += .1f * Time.deltaTime;
    }

    public void exitPatronSound()
    {
        ambianceSource.volume -= .2f * Time.deltaTime;
    }

    public void startBarStaticTheme()
    {
        gameMusicSource.clip = barMusicStatic;
        gameMusicSource.Play();
    }

    //FOCUS FOR AFTER GDC
    //public void startMenuStaticTheme()
    //{
    //    gameMusicSource.clip = menuMusicStatic;
    //    gameMusicSource.Play();
    //}


}
