using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CinemachineFocus : MonoBehaviour
{
    public GameObject previousCamera;
    public GameObject focusCamera;

    [HideInInspector] public UnityEvent onFocusStarted, onFocusEnded;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            previousCamera.SetActive(false);
            focusCamera.SetActive(true);
            onFocusStarted?.Invoke();
        }
    }

    public void StartCinemachineFocus()
    {
        previousCamera.SetActive(false);
        focusCamera.SetActive(true);
        onFocusStarted?.Invoke();
    }

    public void StartTimedCinemachineFocus(float _focusTime)
    {
        StartCoroutine(TimedFocusCoroutine(_focusTime));
    }

    public IEnumerator TimedFocusCoroutine(float _focusTime)
    {
        previousCamera.SetActive(false);
        focusCamera.SetActive(true);
        onFocusStarted?.Invoke();
        yield return new WaitForSecondsRealtime(_focusTime);
        EndCinemachineFocus();
    }

    public void EndCinemachineFocus()
    {
        focusCamera.SetActive(false);
        previousCamera.SetActive(true);
        onFocusEnded?.Invoke();
    }
}
