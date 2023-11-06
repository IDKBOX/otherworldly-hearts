using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    public bool interactionNeeded;
    [Space]
    [HideInInspector] public UnityEvent onDialogueTriggered, onDialogueComplete;
    private bool hasBeenTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenTriggered && !interactionNeeded)
        {
            hasBeenTriggered = true;
            onDialogueTriggered?.Invoke();
            collision.GetComponent<CharacterMovement>().isDisabled = true;
            collision.GetComponent<CharacterMovement>().rb.velocity = Vector3.zero;
            DialogueRunner.Instance.TriggerDialogue(dialogueData);
            DialogueRunner.Instance.currentDialogueTrigger = gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactionNeeded && !hasBeenTriggered && Input.GetKeyDown(KeyCode.E))
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
