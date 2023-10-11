using UnityEngine;
using Cinemachine;

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
