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
            SoundManager.Instance.ChangeMusicVolume(_slider.value);
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        }
        else
        {
            SoundManager.Instance.ChangeSFXVolume(_slider.value);
            _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeSFXVolume(val));
        }
    }
}
