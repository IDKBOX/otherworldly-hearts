using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private bool isMusicSlider;

    void Start()
    {
        if (isMusicSlider)
        {
            SoundManager.Instance.ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume", _slider.value));
            _slider.value = PlayerPrefs.GetFloat("MusicVolume", _slider.value);
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        }
        else
        {
            SoundManager.Instance.ChangeSFXVolume(PlayerPrefs.GetFloat("SFXVolume", _slider.value));
            _slider.value = PlayerPrefs.GetFloat("SFXVolume", _slider.value);
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeSFXVolume(val));
        }
    }
}
