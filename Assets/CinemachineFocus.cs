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

    public void EndCinemachineFocus()
    {
        focusCamera.SetActive(false);
        roomCamera.SetActive(true);
    }
}
