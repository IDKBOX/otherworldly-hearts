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
                if (destroyOnTriggered)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                SoundManager.Instance.FadeOut();
                SoundManager.Instance._musicSource.Stop();
                SoundManager.Instance.isLevelMusicPlaying = false;
                if (destroyOnTriggered)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
