using System.Collections;
using UnityEngine;

public class CinemachineFocus : MonoBehaviour
{
    public GameObject previousCamera;
    public GameObject focusCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            previousCamera.SetActive(false);
            focusCamera.SetActive(true);
        }
    }

    public void StartCinemachineFocus(float _focusTime)
    {
        StartCoroutine(TimedFocusCoroutine(_focusTime));
    }

    public IEnumerator TimedFocusCoroutine(float _focusTime)
    {
        previousCamera.SetActive(false);
        focusCamera.SetActive(true);
        yield return new WaitForSecondsRealtime(_focusTime);
        EndCinemachineFocus();
    }

    public void EndCinemachineFocus()
    {
        focusCamera.SetActive(false);
        previousCamera.SetActive(true);
    }
}
