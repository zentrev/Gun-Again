using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class AudioManager : Singleton<AudioManager>
{
	[SerializeField] Sound[] m_sounds = null;
    [SerializeField] AudioMixerGroup m_music = null;
    [SerializeField] AudioMixerGroup m_sfx = null;

    int curSongLoc = 0;
    bool isPaused = false;
	public override void Awake()
	{
        base.Awake();
		foreach (Sound sound in m_sounds)
		{
			sound.audioSource = gameObject.AddComponent<AudioSource>();
			sound.audioSource.clip = sound.audioClip;
			sound.audioSource.outputAudioMixerGroup = sound.audioMixerGroup;
			sound.audioSource.volume = sound.volume;
			sound.audioSource.pitch = sound.pitch;
			sound.audioSource.loop = sound.loop;
		}
        DontDestroyOnLoad(gameObject);
	}

    private void Start()
    {
        NextSong();
    }

    public void NextSong()
    {
        StopAll();
        curSongLoc++;
        if (curSongLoc > m_sounds.Length - 1)
        {
            curSongLoc = 0;
        }
        PlayThrough();
    }
    public void PrevSong()
    {
        StopAll();
        curSongLoc--;
        if(curSongLoc < 0)
        {
            curSongLoc = m_sounds.Length - 1;
        }
        PlayThrough();
    }

    public void Play(string name)
	{
		Sound sound = Array.Find(m_sounds, s => s.name == name);
        if (sound.volume == 1)
        {
            if (sound != null)
            {
                sound.audioSource.Play();
            }
        }
	}

    public void PlayThrough()
    {
        if(isPaused)
        {
            Resume();
        }
        else
        {
            Sound sound = m_sounds[curSongLoc];
            if (sound != null)
            {
                if(sound.audioMixerGroup == m_music)
                {
                    sound.audioSource.Play();
                }
            }
        }
    }
    public void Pause()
    {
        Sound sound = m_sounds[curSongLoc];
        if (sound != null)
        {
            sound.audioSource.Pause();
        }

        isPaused = true;
    }

    public void Resume()
    {
        Sound sound = m_sounds[curSongLoc];
        if (sound != null)
        {
            sound.audioSource.UnPause();
        }
        isPaused = false;
    }

    private void Update()
    {

        if (curSongLoc > m_sounds.Length - 1)
        {
            curSongLoc = 0;
        }
        if(curSongLoc < 0)
        {
            curSongLoc = m_sounds.Length-1;
        }
        if(!isPaused)
        {
            if (!m_sounds[curSongLoc].audioSource.isPlaying)
            {
                curSongLoc++;
                if (curSongLoc > m_sounds.Length - 1)
                {
                    curSongLoc = 0;
                }
                PlayThrough();
            }
        }
    }

    public void StopAll()
    {
        foreach(Sound sound in m_sounds)
        {
            sound.audioSource.Stop();
        }
    }

    public void ToggleMusic()
    {
        for(int i = 0; i < m_sounds.Length; i++)
        {
            if(m_sounds[i].name == "Music")
            {
                if(m_sounds[i].volume == 0)
                {
                    m_sounds[i].volume = 1;
                    m_sounds[i].audioSource.UnPause();
                    Debug.Log("vol set to 1");
                }
                else
                {
                    m_sounds[i].volume = 0;
                    m_sounds[i].audioSource.Pause();
                    Debug.Log("vol set to 0");
                }
            }
        }

    }

    public void ToggleSFX()
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].name == "SoundFX")
            {
                if (m_sounds[i].volume == 0)
                {
                    m_sounds[i].volume = 1;
                    Debug.Log("sfx set to 100");
                }
                else
                {
                    m_sounds[i].volume = 0;
                    Debug.Log("sfx set to 0");
                }
            }
        }
    }
}
