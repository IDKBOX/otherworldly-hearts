using UnityEngine;

public class MenuMusicPlayer : MonoBehaviour
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
