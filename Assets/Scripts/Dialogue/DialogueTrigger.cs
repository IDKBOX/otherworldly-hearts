using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    public UnityEvent onDialogueTriggered, onDialogueComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onDialogueTriggered?.Invoke();
            collision.GetComponent<CharacterMovement>().isDisabled = true;
            collision.GetComponent<CharacterMovement>().rb.velocity = Vector3.zero;
            DialogueRunnerV2.Instance.TriggerDialogue(dialogueData);
            DialogueRunnerV2.Instance.currentDialogueTrigger = gameObject;
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
