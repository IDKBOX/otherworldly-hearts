using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public Volume volume;

    void Start()
    {
        LiftGammaGain liftGammaGain;

        if (volume.profile.TryGet<LiftGammaGain>(out liftGammaGain))
        {
            _slider.value = PlayerPrefs.GetFloat("BrightnessValue", 0.2f);
            _slider.onValueChanged.AddListener(val => liftGammaGain.gamma.Override(new Vector4(1f, 1f, 1f, val)));
            _slider.onValueChanged.AddListener(val => PlayerPrefs.SetFloat("BrightnessValue",_slider.value));
        }
    }
}
