using UnityEngine;
using UnityEngine.InputSystem;

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
    public AudioClip SFXAltarAddItem;

    [Header("Dialog Trigger")]
    public DialogueStarter afterLevel1;
    public DialogueStarter afterLevel2;
    public DialogueStarter afterLevel3;

    private bool inTrigger;
    private bool hasBeenTriggered;

    //new input system
    private PlayerControls playerControls;
    private InputAction interact;



    private void Awake()
    {
        altarEffect = GetComponentInChildren<Animator>();
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

    private void Start()
    {
        AltarFirstDialog();
    }

    private void Update()
    {
        if (inTrigger && !hasBeenTriggered && interact.triggered)
        {
            hasBeenTriggered = true;
            Destroy(interactPromptPrefab);
            ShowInteractButton.Instance.DisableInteractButton();
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
                SoundManager.Instance.PlaySound(SFXAltarAddItem);
                OneItemDialogue.StartDialogue();
                level3Door.UnlockDoor();
                altarAnimSprite.sprite = photoSprite;
                break;
            case 2:
                SoundManager.Instance.PlaySound(SFXAltarAddItem);
                TwoItemsDialogue.StartDialogue();
                altarAnimSprite.sprite = diarySprite;
                break;
        }
    }

    private void AltarFirstDialog()
    {
        switch (ItemDisplay.itemsToShow)
        {
            default:
                afterLevel1.StartDialogue();
                break;
            case 1:
                afterLevel2.StartDialogue();
                break;
            case 2:
                afterLevel3.StartDialogue();
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

            if (!hasBeenTriggered)
            {
                ShowInteractButton.Instance.EnableInteractButton();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;

            if (interactPromptPrefab != null)
            {
                interactPromptPrefab.SetActive(false);
            }
            ShowInteractButton.Instance.DisableInteractButton();
        }
    }

}
