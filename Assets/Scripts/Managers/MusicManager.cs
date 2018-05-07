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

    private IMusicState barRising;
    private IMusicState barFading;
    private IMusicState menuRising;
    private IMusicState menuSetting;
    private IMusicState playOn;
    private IMusicState currentMusicState;

    private float maxAmbiance;

    public float maxMusicVolume;

    public void initMusicManager()
    {
        barRising = new BarMusicRising(this);
        barFading = new BarMusicFading(this);
        menuRising = new MenuMusicRise(this);
        menuSetting = new MenuMusicFade(this);
        playOn = new MusicPlays();

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
        
        if (musicManager.gameMusicSource.volume < musicManager.maxMusicVolume) 
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
        if (0 >= musicManager.gameMusicSource.volume)
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
        if (musicManager.gameMusicSource.volume > musicManager.maxMusicVolume)
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

