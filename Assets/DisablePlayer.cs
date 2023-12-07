using UnityEngine;

public class DisablePlayer : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindAnyObjectByType<CharacterMovement>().isDisabled = true;
    }
}
