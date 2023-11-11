using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicToPlay;

    void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMusic(musicToPlay);
        }
    }
}
