using UnityEngine;

public class CinemachineFocus : MonoBehaviour
{
    public GameObject roomCamera;
    public GameObject focusCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            roomCamera.SetActive(false);
            focusCamera.SetActive(true);
        }
    }
/*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            focusCamera.SetActive(false);
            roomCamera.SetActive(true);
        }
    }*/

    private void OnDestroy()
    {
        focusCamera.SetActive(false);
        roomCamera.SetActive(true);
    }
}
