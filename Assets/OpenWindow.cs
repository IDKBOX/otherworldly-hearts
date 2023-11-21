using System.Collections;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    public GameObject windowClosed;
    public GameObject windowOpened;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        windowClosed.SetActive(false);
        windowOpened.SetActive(true);
        CinemachineShake.Instance.ShakeCamera(0, 0.1f);
        Destroy(gameObject, 3f);
    }
}
