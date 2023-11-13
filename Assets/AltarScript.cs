using UnityEngine;

public class AltarScript : MonoBehaviour
{
    [Header("Dialogues")]
    public DialogueStarter NoItemsDialogue;
    public DialogueStarter OneItemDialogue;
    public DialogueStarter TwoItemsDialogue;

    [Header("Prerequisites")]
    public DoorScript level2Door;
    public DoorScript level3Door;
    public GameObject interactPromptPrefab;

    private bool inTrigger;
    private bool hasBeenTriggered;
    private Animator animator;

    private void Update()
    {
        if (inTrigger && !hasBeenTriggered && Input.GetKeyDown(KeyCode.E))
        {
            hasBeenTriggered = true;
            Destroy(interactPromptPrefab);
            AltarInteract();
        }
    }

    private void AltarInteract()
    {
        switch (ItemDisplay.itemsToShow)
        {
            case 0:
                NoItemsDialogue.StartDialogue();
                level2Door.UnlockDoor();
                break;
            case 1:
                OneItemDialogue.StartDialogue();
                level3Door.UnlockDoor();
                break;
            case 2:
                TwoItemsDialogue.StartDialogue();
                break;
            default:
                Debug.Log("Altar Error");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = true;

            if (interactPromptPrefab != null)
            {
                interactPromptPrefab.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            hasBeenTriggered = false;

            if (interactPromptPrefab != null)
            {
                interactPromptPrefab.SetActive(false);
            }
        }
    }

}
