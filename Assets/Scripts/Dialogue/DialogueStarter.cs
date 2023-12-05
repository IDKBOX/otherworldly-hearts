using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DialogueStarter : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    public bool destroyOnComplete = true;
    public bool beginOnStart;
    public UnityEvent onDialogueTriggered, onDialogueComplete;
    [HideInInspector] public bool hasBeenTriggered;

    private void Start()
    {
        if (beginOnStart)
        {
            StartDialogue();
        }
    }

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
