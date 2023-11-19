using UnityEngine;

public class AltarScript : MonoBehaviour
{
/*    [Header("Altar Sprites")]
    public Sprite altarNoItems;
    public Sprite altarOneItem;
    public Sprite altarTwoItems;*/

    [Header("ItemSprites")]
    public Sprite photoSprite;
    public Sprite diarySprite;

    [Header("Dialogues")]
    public DialogueStarter NoItemsDialogue;
    public DialogueStarter OneItemDialogue;
    public DialogueStarter TwoItemsDialogue;

    [Header("Prerequisites")]
    public DoorScript level2Door;
    public DoorScript level3Door;
    public GameObject interactPromptPrefab;
    public Animator altarEffect;
    public SpriteRenderer altarAnimSprite;

    private bool inTrigger;
    private bool hasBeenTriggered;
    
    private void Awake()
    {
        altarEffect = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (inTrigger && !hasBeenTriggered && Input.GetKeyDown(KeyCode.E))
        {
            hasBeenTriggered = true;
            Destroy(interactPromptPrefab);
            altarEffect.SetTrigger("Diminish");
            AltarInteract();
        }
    }

    private void AltarInteract()
    {
        switch (ItemDisplay.itemsToShow)
        {
            default:
                NoItemsDialogue.StartDialogue();
                level2Door.UnlockDoor();
                break;
            case 1:
                OneItemDialogue.StartDialogue();
                level3Door.UnlockDoor();
                altarAnimSprite.sprite = photoSprite;
                break;
            case 2:
                TwoItemsDialogue.StartDialogue();
                altarAnimSprite.sprite = diarySprite;
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