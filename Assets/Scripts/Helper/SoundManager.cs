using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectSource, _overlapEffectSource, _ambientSource;

    //audio fade snapshots
    public AudioMixerSnapshot normalAudioSnapshot;
    public AudioMixerSnapshot noMusicAudioSnapshot;

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
        ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1));
        ChangeSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 1));
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
}
