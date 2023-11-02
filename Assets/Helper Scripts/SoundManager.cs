using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectSource, _overlapEffectSource;

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
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
