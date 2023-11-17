using UnityEngine;

public class ShakeCameraContinuously : MonoBehaviour
{
    public void StartCameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(5f, float.PositiveInfinity);
    }

    public void StopCameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(0f, 0.1f);
    }
}
