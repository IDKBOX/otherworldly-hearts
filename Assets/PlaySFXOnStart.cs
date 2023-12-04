using UnityEngine;

public class PlaySFXOnStart : MonoBehaviour
{
    public AudioClip SFXToPlay;

    void Start()
    {
        SoundManager.Instance.PlaySound(SFXToPlay);
    }
}
