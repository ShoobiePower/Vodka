using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [Tooltip("The amount the music changes by")]
    public float ammountTochangeBy;

    [Tooltip("The amount the chatter changes by")]
    [SerializeField]
    float ammountToChangeChatterBy;
    public float AmmountToChangeChatterBy { get { return ammountToChangeChatterBy; } }

    public AudioSource gameMusicSource;
    public AudioSource ambianceSource;
    public AudioClip barMusicLayered;
    public AudioClip barMusicStatic;
    public AudioClip menuMusicStatic;

    private IMusicState barRising;
    private IMusicState barFading;
    private IMusicState menuRising;
    private IMusicState menuSetting;
    private IMusicState playOn;
    private IMusicState currentMusicState;

    [SerializeField]
    float maxAmbiance;
    public float MaxAmbiance { get { return maxAmbiance; } }

    public AudioMixerGroup AmbianceMixerGroup;

    [SerializeField]
    float maxMusicVolume;
    public float MaxMusicVolume { get { return maxMusicVolume; } }

    [HideInInspector]
    public float chatterGainIsAt;
    // public float GainIsAt { get { return gainIsAt; } set { gainIsAt = value; }  }

    private const float minimumChatterVolume = -80f;
    public float MinimumChatterVolume { get { return minimumChatterVolume; } }

    public void initMusicManager()
    {
        barRising = new BarMusicRising(this);
        barFading = new BarMusicFading(this);
        menuRising = new MenuMusicRise(this);
        menuSetting = new MenuMusicFade(this);
        playOn = new MusicPlays();

        chatterGainIsAt = minimumChatterVolume;

        ambianceSource.outputAudioMixerGroup = AmbianceMixerGroup;

        currentMusicState = GetBarRising();
        currentMusicState.StartPart();
}

    public void setCurrentMusicState(IMusicState musicStateToSwapTo)
    {
        currentMusicState = musicStateToSwapTo;
        StartCurrentSong();
    }

    public IMusicState GetBarRising()
    {
        return barRising;
    }

    public IMusicState GetBarFading()
    {
        return barFading;
    }

    public IMusicState GetMenuRising()
    {
        return menuRising;
    }

    public IMusicState GetMenuSetting()
    {
        return menuSetting;
    }

    public IMusicState GetPlayThrough()
    {
        return playOn;
    }

    public void StartCurrentSong()
    {
        currentMusicState.StartPart();
    }

    // Update is called once per frame
    void Update()
    {
        currentMusicState.ChangeVolume();
    }

    public void startBarStaticTheme()
    {
        gameMusicSource.clip = barMusicStatic;
        gameMusicSource.Play();
    }

    public void setMusicVolume(float newMaxVolume)
    {
        maxMusicVolume = newMaxVolume;
        gameMusicSource.volume = newMaxVolume;
    }

}

public interface IMusicState
{
    void StartPart();
    void ChangeVolume();
    void OnEnd();
}

public class BarMusicRising : IMusicState
{
    private MusicManager musicManager;
    float musicLoopCountDown;

    public BarMusicRising(MusicManager musicManager)
    {
        this.musicManager = musicManager;
    }

    public void ChangeVolume()
    {
        checkOnAmbiance();
        if (musicManager.gameMusicSource.volume < musicManager.MaxMusicVolume) 
        {
            musicManager.gameMusicSource.volume += musicManager.ammountTochangeBy * Time.deltaTime;
        }
        tickTimeDown();

    }

    public void OnEnd()
    {
    }

    public void StartPart()
    {
        musicManager.gameMusicSource.Stop();
        musicManager.gameMusicSource.clip = musicManager.barMusicLayered;
        musicManager.gameMusicSource.Play();
        musicLoopCountDown = musicManager.gameMusicSource.clip.length;
    }

    private void tickTimeDown()
    {
        musicLoopCountDown -= Time.deltaTime;
        if (musicLoopCountDown <= 0)
        {
            musicManager.gameMusicSource.Stop();
            musicManager.gameMusicSource.clip = musicManager.barMusicStatic;
            musicManager.gameMusicSource.Play();
            musicManager.setCurrentMusicState(musicManager.GetPlayThrough());
        }
    }

    private void checkOnAmbiance() // It acts the same as a music, but I think this should go somwhere else as it is classified as an sfx;
    {     
        if ( musicManager.chatterGainIsAt < musicManager.MaxAmbiance)
        {
            musicManager.AmbianceMixerGroup.audioMixer.GetFloat("Chatter", out musicManager.chatterGainIsAt);
            musicManager.AmbianceMixerGroup.audioMixer.SetFloat("Chatter", (musicManager.chatterGainIsAt += (musicManager.AmmountToChangeChatterBy * Time.deltaTime)));
        }
    }
}

public class BarMusicFading : IMusicState
{

    private MusicManager musicManager;
    public BarMusicFading(MusicManager musicManager)
    {
        this.musicManager = musicManager;
    }

    public void ChangeVolume()
    {
        musicManager.gameMusicSource.volume -= musicManager.ammountTochangeBy * Time.deltaTime;
        checkOnAmbiance();

        if (0 >= musicManager.gameMusicSource.volume && musicManager.chatterGainIsAt <= musicManager.MinimumChatterVolume)
        {
            OnEnd();
        }
    }

    public void OnEnd()
    {
        musicManager.setCurrentMusicState(musicManager.GetMenuRising());
    }

    public void StartPart()
    {
        
    }

    private void checkOnAmbiance() // It acts the same as a music, but I think this should go somwhere else as it is classified as an sfx;
    {
        musicManager.AmbianceMixerGroup.audioMixer.GetFloat("Chatter", out musicManager.chatterGainIsAt);
        musicManager.AmbianceMixerGroup.audioMixer.SetFloat("Chatter", (musicManager.chatterGainIsAt -= (musicManager.AmmountToChangeChatterBy * Time.deltaTime)));
    }
}


public class MenuMusicRise : IMusicState
{

    private MusicManager musicManager;
    public MenuMusicRise(MusicManager musicManager)
    {
        this.musicManager = musicManager;
    }

    public void ChangeVolume()
    {
        musicManager.gameMusicSource.volume += musicManager.ammountTochangeBy * Time.deltaTime;
        if (musicManager.gameMusicSource.volume > musicManager.MaxMusicVolume)
        {
            OnEnd();
        }
    }

    public void OnEnd()
    {
        musicManager.setCurrentMusicState(musicManager.GetPlayThrough());
    }

    public void StartPart()
    {
        musicManager.gameMusicSource.Stop();
        musicManager.gameMusicSource.clip = musicManager.menuMusicStatic;
        musicManager.gameMusicSource.Play();
    }
}


public class MenuMusicFade : IMusicState
{

    private MusicManager musicManager;
    public MenuMusicFade(MusicManager musicManager)
    {
        this.musicManager = musicManager;
    }

    public void ChangeVolume()
    {
        musicManager.gameMusicSource.volume -= musicManager.ammountTochangeBy * Time.deltaTime;
        if (0 >= musicManager.gameMusicSource.volume)
        {
            OnEnd();
        }
    }

    public void OnEnd()
    {
        musicManager.setCurrentMusicState(musicManager.GetBarRising());
    }

    public void StartPart()
    {
    }
}

public class MusicPlays : IMusicState
{
    public void ChangeVolume()
    {

    }

    public void OnEnd()
    {

    }

    public void StartPart()
    {
    }
}

