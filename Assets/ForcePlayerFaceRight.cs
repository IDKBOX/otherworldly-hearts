using UnityEngine;

public class ForcePlayerFaceRight : MonoBehaviour
{
    private void Start()
    {
        var characterMovement = FindAnyObjectByType<CharacterMovement>();
        Debug.Log(characterMovement.isFacingRight);
        if (!characterMovement.isFacingRight)
        {
            Vector3 localScale = characterMovement.transform.localScale;
            localScale.x = 1f;
            Debug.Log("Flipped");
        }
    }
}
