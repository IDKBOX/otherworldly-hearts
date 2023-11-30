using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    public bool interactionNeeded;
    public bool destroyOnComplete = true;
    [Space]
    public UnityEvent onDialogueTriggered;
    private bool hasBeenTriggered;
    private bool inTrigger;

    [Header("Interaction Prerequisite (Optional)")]
    public GameObject interactPromptPrefab;

    //new input system
    private PlayerControls playerControls;
    private InputAction interact;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        interact = playerControls.Player.Interact;
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }

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
        if (inTrigger && interactionNeeded && !hasBeenTriggered && interact.triggered)
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
}
