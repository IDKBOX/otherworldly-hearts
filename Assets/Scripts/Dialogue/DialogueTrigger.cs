using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    public UnityEvent onDialogueTriggered, onDialogueComplete;
    private bool hasBeenTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true;
            onDialogueTriggered?.Invoke();
            collision.GetComponent<CharacterMovement>().isDisabled = true;
            collision.GetComponent<CharacterMovement>().rb.velocity = Vector3.zero;
            DialogueRunner.Instance.TriggerDialogue(dialogueData);
            DialogueRunner.Instance.currentDialogueTrigger = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onDialogueComplete?.Invoke();
        }
    }
}
