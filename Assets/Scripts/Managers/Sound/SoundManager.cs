using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;

    public static SoundManager Instance { get { return instance; } }

    static System.Object padLock = new System.Object();

    public static Dictionary<string, MonoBehaviour> sounds;

    static Queue<string> AudioCommandQueue;

    // Use this for initialization
    void Awake()
    {
        sounds = new Dictionary<string, MonoBehaviour>();
        AudioCommandQueue = new Queue<string>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        foreach (AudioCommand ac in this.GetComponents<AudioCommand>())
        {
            SoundManager.sounds.Add(ac.CommandText, ac);
        }

    }

    // Update is called once per frame
    void Update()
    {

        while (AudioCommandQueue.Count > 0)
        {
            AudioCommand ac = sounds[AudioCommandQueue.Dequeue()] as AudioCommand;
            ac.Play();
        }

    }

    public void AddCommand(string command)
    {
        AudioCommandQueue.Enqueue(command);
    }
}

