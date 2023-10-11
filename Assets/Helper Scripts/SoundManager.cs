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
}
