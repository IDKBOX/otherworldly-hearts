using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OpenWindow : MonoBehaviour
{
    public GameObject windowClosed;
    public GameObject windowOpened;
    public UnityEvent onComplete;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        windowClosed.SetActive(false);
        windowOpened.SetActive(true);
        CinemachineShake.Instance.ShakeCamera(0, 0.1f);
        yield return new WaitForSeconds(1);
        onComplete?.Invoke();
        Destroy(gameObject, 3f);
    }
}
