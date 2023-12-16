using UnityEngine;

public class DisablePlayer : MonoBehaviour
{
    private void Start()
    {
        Disable();
    }

    public void Disable()
    {
        GameObject.FindAnyObjectByType<CharacterMovement>().isDisabled = true;

        if (GameObject.Find("MobileUI") != null)
        {
            GameObject.Find("MobileUI").SetActive(false);
        }

        if (GameObject.Find("GameplayUI") != null)
        {
            GameObject.Find("GameplayUI").SetActive(false);
        }
        
        PauseManager.canPause = false;
    }
}
