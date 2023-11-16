using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicToPlay;
    public bool playMusic = true;

    void Start()
    {
        if (SoundManager.Instance.isLevelMusicPlaying == false && playMusic)
        {
            SoundManager.Instance.isLevelMusicPlaying = true;
            SoundManager.Instance.PlayMusic(musicToPlay);
        }

        if (!playMusic)
        {
            SoundManager.Instance.FadeOut();
            SoundManager.Instance._musicSource.Stop();
            SoundManager.Instance.isLevelMusicPlaying = false;
        }
    }
}
