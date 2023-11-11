using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip musicToPlay;

    private bool hasBeenTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true;

            if (musicToPlay != null)
            {
                SoundManager.Instance.PlayMusic(musicToPlay);
                Destroy(gameObject);
            }
            else
            {
                SoundManager.Instance.FadeOut();
                Destroy(gameObject);
            }
        }
    }
}
