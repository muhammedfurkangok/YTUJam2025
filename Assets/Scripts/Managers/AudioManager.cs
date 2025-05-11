using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    ColtShoot = 0,
    ColtReload = 1,
    ShotgunShoot  = 2,
    ShotgunReload = 3,
    FistHit = 5,
    Walk = 4,
    TiredBreath = 6,
}

[Serializable]
public class GameSound
{
    public SoundType key;
    public bool isMultiple;
    public List<AudioClip> clips;
    public AudioSource externalAudioSource;
    public float volume = 1.0f;
    public bool useRandomPitch = false;
    public float minPitch = 1.0f;
    public float maxPitch = 1.0f;
    

    public AudioClip GetRandomClip()
    {
        if (clips == null || clips.Count == 0) return null;
        return clips[UnityEngine.Random.Range(0, clips.Count)];
    }
}

public class AudioManager : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private AudioSource mainAudioSource;
    public AudioSource mainMusicSource;

    [SerializeField] private List<GameSound> gameSounds = new();

    public static AudioManager Instance;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayOneShotSound(SoundType key)
    {
        var gameSound = gameSounds.Find(x => x.key == key);
        if (gameSound == null) return;

        AudioClip clip = gameSound.isMultiple ? gameSound.GetRandomClip() : gameSound.clips[0];
        if (clip == null) return;

        float originalPitch = mainAudioSource.pitch;
        if (gameSound.useRandomPitch)
        {
            mainAudioSource.pitch = UnityEngine.Random.Range(gameSound.minPitch, gameSound.maxPitch);
        }

        if (gameSound.externalAudioSource != null && !gameSound.externalAudioSource.isPlaying)
        {
            gameSound.externalAudioSource.PlayOneShot(clip, gameSound.volume);
        }
        else
        {
            mainAudioSource.PlayOneShot(clip, gameSound.volume);
        }

        if (gameSound.useRandomPitch)
        {
            mainAudioSource.pitch = originalPitch;
        }
    }

  private int lastPlayedIndex = -1;

public void PlayOneShotSound(SoundType key, float volume)
{
    var gameSound = gameSounds.Find(x => x.key == key);
    if (gameSound == null) return;

    AudioClip clip = null;
    if (gameSound.isMultiple)
    {
        int newIndex;
        do
        {
            newIndex = UnityEngine.Random.Range(0, gameSound.clips.Count);
        } while (newIndex == lastPlayedIndex && gameSound.clips.Count > 1);
        lastPlayedIndex = newIndex;
        clip = gameSound.clips[newIndex];
    }
    else
    {
        clip = gameSound.clips[0];
    }

    if (clip == null) return;

    float originalPitch = mainAudioSource.pitch;
    if (gameSound.useRandomPitch)
    {
        mainAudioSource.pitch = UnityEngine.Random.Range(gameSound.minPitch, gameSound.maxPitch);
    }

    if (gameSound.externalAudioSource != null)
    {
        if (gameSound.externalAudioSource.isPlaying) return;
        gameSound.externalAudioSource.PlayOneShot(clip, volume);
    }
    else
    {
        mainAudioSource.PlayOneShot(clip, volume);
    }

    if (gameSound.useRandomPitch)
    {
        mainAudioSource.pitch = originalPitch;
    }
}
    public GameSound GetGameSound(SoundType walk)
    {
        return gameSounds.Find(x => x.key == walk);
    }
    
    public void PlayMainMusic()
    {
        mainMusicSource.Play();
    }
}