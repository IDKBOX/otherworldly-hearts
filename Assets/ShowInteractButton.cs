using UnityEngine;

public class ShowInteractButton : MonoBehaviour
{
    public static ShowInteractButton Instance;
    public GameObject interactButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnableInteractButton()
    {
        interactButton.SetActive(true);
    }

    public void DisableInteractButton()
    {
        interactButton.SetActive(false);
    }
}
