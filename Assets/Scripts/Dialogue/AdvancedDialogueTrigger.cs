using UnityEngine;
using UnityEngine.Events;

public class AdvancedDialogueTrigger : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    public bool interactionNeeded;
    public bool destroyOnComplete = true;
    [Space]
    public UnityEvent onDialogueTriggered, onDialogueComplete;
    private bool hasBeenTriggered;
    private bool inTrigger;

    [Header("Interaction Prerequisite (Optional)")]
    public GameObject interactPromptPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = true;

            if (!hasBeenTriggered && !interactionNeeded)
            {
                hasBeenTriggered = true;
                onDialogueTriggered?.Invoke();
                collision.GetComponent<CharacterMovement>().isDisabled = true;
                collision.GetComponent<CharacterMovement>().rb.velocity = Vector3.zero;
                DialogueRunner.Instance.TriggerDialogue(dialogueData);
                DialogueRunner.Instance.currentDialogueTrigger = gameObject;
            }
            else if (!hasBeenTriggered && interactionNeeded && interactPromptPrefab != null)
            {
                interactPromptPrefab.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (inTrigger && interactionNeeded && !hasBeenTriggered && Input.GetKeyDown(KeyCode.E))
        {
            if (interactPromptPrefab != null)
            {
                interactPromptPrefab.SetActive(false);
            }

            hasBeenTriggered = true;
            onDialogueTriggered?.Invoke();
            FindAnyObjectByType<CharacterMovement>().isDisabled = true;
            FindAnyObjectByType<CharacterMovement>().rb.velocity = Vector3.zero;
            DialogueRunner.Instance.TriggerDialogue(dialogueData);
            DialogueRunner.Instance.currentDialogueTrigger = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            
            if (interactionNeeded)
            {
                hasBeenTriggered = false;
            }

            if (interactPromptPrefab != null)
            {
                interactPromptPrefab.SetActive(false);
            }
        }
    }

    public void OnComplete()
    {
        onDialogueComplete?.Invoke();
    }
}
