using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicToPlay;

    void Start()
    {
        SoundManager.Instance.PlayMusic(musicToPlay);
    }
}
