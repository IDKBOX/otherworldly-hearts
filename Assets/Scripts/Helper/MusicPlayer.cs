using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicToPlay;
    public bool playMusic = true;

    void Start()
    {
        if (SoundManager.Instance != null && playMusic)
        {
            SoundManager.Instance.PlayMusic(musicToPlay);
        }
        else
        {
            SoundManager.Instance.FadeOut();
            SoundManager.Instance._musicSource.Stop();
        }
    }
}
