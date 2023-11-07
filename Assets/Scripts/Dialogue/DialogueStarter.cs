using UnityEngine;
using UnityEngine.Events;

public class DialogueStarter : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    public UnityEvent onDialogueTriggered, onDialogueComplete;
    private bool hasBeenTriggered;

    public void StartDialogue()
    {
        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;
            onDialogueTriggered?.Invoke();
            FindAnyObjectByType<CharacterMovement>().isDisabled = true;
            FindAnyObjectByType<CharacterMovement>().rb.velocity = Vector3.zero;
            DialogueRunner.Instance.TriggerDialogue(dialogueData);
            DialogueRunner.Instance.currentDialogueTrigger = gameObject;
        }
    }

    private void OnDestroy()
    {
        onDialogueComplete?.Invoke();
    }
}
