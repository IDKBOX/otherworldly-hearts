using UnityEngine;
using System.Collections;

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
            StartCoroutine(FadeOutMusic());
        }
    }

    private IEnumerator FadeOutMusic()
    {
        SoundManager.Instance.FadeOut();
        yield return new WaitForSecondsRealtime(0.5f);
        SoundManager.Instance._musicSource.Stop();
        SoundManager.Instance.isLevelMusicPlaying = false;
    }
}
