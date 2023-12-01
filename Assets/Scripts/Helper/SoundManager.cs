using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource _musicSource, _effectSource, _overlapEffectSource, _overlapEffectSource2, _ambientSource, _ambientSource2;

    //audio fade snapshots
    public AudioMixerSnapshot normalAudioSnapshot;
    public AudioMixerSnapshot noMusicAudioSnapshot;
    [HideInInspector] public bool isLevelMusicPlaying;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.5f));
        ChangeSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0.5f));
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _effectSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("No Audio Clip!");
        }
    }

    public void PlayOverlappingSound(AudioClip clip)
    {
        if (clip != null && !_overlapEffectSource.isPlaying)
        {
            _overlapEffectSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("No Audio Clip!");
        }
    }

    public void PlayOverlappingSound2(AudioClip clip)
    {
        if (clip != null && !_overlapEffectSource2.isPlaying)
        {
            _overlapEffectSource2.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("No Audio Clip!");
        }
    }

    public void PlayMusic(AudioClip _clip)
    {
        if (_clip != null)
        {
            _musicSource.clip = _clip;
            _musicSource.Play();
            FadeIn();
        }
        else
        {
            Debug.LogWarning("No Audio Clip!");
        }
    }

    public void ChangeMusicVolume(float value)
    {
       _musicSource.volume = value;
       PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ChangeSFXVolume(float value)
    {
        _effectSource.volume = value;
        _overlapEffectSource.volume = value;
        _ambientSource.volume = value;
        _overlapEffectSource2.volume = value;
        _ambientSource2.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void FadeIn()
    {
        normalAudioSnapshot.TransitionTo(1f);
    }

    public void FadeOut()
    {
        noMusicAudioSnapshot.TransitionTo(0.5f);
    }

    public void FadeOutLong()
    {
        noMusicAudioSnapshot.TransitionTo(2f);
    }
}
