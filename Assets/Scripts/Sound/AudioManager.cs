using System.Collections;
using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public Sound[] sounds;
    public Sound[] songs;

    // Start is called before the first frame update
    void Awake()
    {
        if (setSingleton())
        {
            setupSounds();
            setupSongs();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Tried to play a sound that didn't exist! Tried to play sound: " + name);
            return;
        }

        s.source.Play();
    }

    public Sound PlaySong(string name)
    {
        Sound s = Array.Find(songs, song => song.name == name);

        if (s == null)
        {
            Debug.LogWarning("Tried to play a song that didn't exist! Tried to play song: " + name);
            return null;
        }

        s.source.Play();
        return s;
    }

    private void Start()
    {
        StartMusic();
    }

    private void StartMusic()
    {
        StartCoroutine(LoopMusic());
    }

    private IEnumerator LoopMusic()
    {
        int songIndex = UnityEngine.Random.Range(0, songs.Length);
        Sound activeSong = PlaySong(songs[songIndex].name);

        while (true)
        {
            yield return new WaitForSeconds(activeSong.clip.length);

            if (songIndex == songs.Length - 1)
            {
                songIndex = 0;
            } else
            {
                songIndex++;
            }

            activeSong = PlaySong(songs[songIndex].name);
        }
    }

    void setupSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void setupSongs()
    {
        foreach (Sound s in songs)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    bool setSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return true;
        }

        Destroy(gameObject);
        return false;
    }
}
