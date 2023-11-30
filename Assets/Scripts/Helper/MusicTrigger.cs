using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip musicToPlay;
    public bool playMusic;
    public bool destroyOnTriggered;

    private bool hasBeenTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true;

            if (playMusic)
            {
                SoundManager.Instance.PlayMusic(musicToPlay);
                destroyTrigger();
            }
            else
            {
                StartCoroutine(FadeOutMusic());
                destroyTrigger();
            }
        }
    }

    private void destroyTrigger()
    {
        if (destroyOnTriggered)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FadeOutMusic()
    {
        SoundManager.Instance.FadeOutLong();
        yield return new WaitForSecondsRealtime(2f);
        SoundManager.Instance._musicSource.Stop();
        SoundManager.Instance.isLevelMusicPlaying = false;
    }
}
