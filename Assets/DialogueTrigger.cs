using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterMovement>().isDisabled = true;
            DialogueRunnerV2.Instance.TriggerDialogue(dialogueData);
            Destroy(gameObject);
        }
    }
}
