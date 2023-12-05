using UnityEngine;

public class ShakeCameraContinuously : MonoBehaviour
{
    public float shakeValue = 5f;
    public bool enableOnStart;
    private bool shakeStarted;

    [Header("Optional")]
    public AudioClip SFXShake;
    

    private void Start()
    {
        if (enableOnStart)
        {
            StartCameraShake();
        }
    }

    private void Update()
    {
        if (shakeStarted)
        {
            StartCameraShake();
        }
    }

    public void StartCameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(shakeValue, float.PositiveInfinity);
        shakeStarted = true;
    }

    public void StartCameraShakeWithSFX()
    {
        CinemachineShake.Instance.ShakeCamera(shakeValue, float.PositiveInfinity);

        if (SFXShake != null)
        {
            SoundManager.Instance.PlaySound(SFXShake);
        }
    }


    public void StopCameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(0f, 0.1f);
        shakeStarted= false;
    }
}
