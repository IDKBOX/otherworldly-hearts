using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance {get; private set;}
    public RectTransform GameUI;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    //on awake, set this object as the cinemachineVirtualCamera
    private void Awake() {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        //gets the basic multi channel perlin from cinemachine virtual camera
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;

        GameUI.DOScale(1f, 0);
        float _punchIntensity = intensity * .005f;
        GameUI.DOPunchScale(new Vector3(_punchIntensity, _punchIntensity, 0), .3f);
    }

    private void Update() {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0f)
                {
                    // timer over!
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
        }
    }
}
