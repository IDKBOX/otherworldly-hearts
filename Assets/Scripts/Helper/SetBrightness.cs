using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class SetBrightness : MonoBehaviour
{
    public Volume volume;

    void Start()
    {
        LiftGammaGain liftGammaGain;

        if (volume.profile.TryGet<LiftGammaGain>(out liftGammaGain))
        {
            liftGammaGain.gamma.Override(new Vector4(1f, 1f, 1f, PlayerPrefs.GetFloat("BrightnessValue", 0.2f)));
        }
    }
}
