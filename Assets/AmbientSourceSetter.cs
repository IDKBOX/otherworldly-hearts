using UnityEngine;

public class AmbientSourceSetter : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SoundManager.Instance._ambientSource2 = audioSource;
        audioSource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }
}
