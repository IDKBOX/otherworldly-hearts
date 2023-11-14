using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DialogueStarter : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    public bool destroyOnComplete = true;
    public UnityEvent onDialogueTriggered, onDialogueComplete;
    [HideInInspector] public bool hasBeenTriggered;

    public void StartDialogue()
    {
        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;
            onDialogueTriggered?.Invoke();
            FindAnyObjectByType<CharacterMovement>().isDisabled = true;
            FindAnyObjectByType<CharacterMovement>().rb.velocity = Vector3.zero;
            DialogueRunner.Instance.TriggerDialogue(dialogueData);
            StartCoroutine(setCurrentDialogueTrigger());
        }
    }

    public void OnComplete()
    {
        onDialogueComplete?.Invoke();
    }

    IEnumerator setCurrentDialogueTrigger()
    {
        yield return new WaitForSeconds(0.2f);
        DialogueRunner.Instance.currentDialogueTrigger = gameObject;
    }
}
