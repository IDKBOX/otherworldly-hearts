using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AltarScript : MonoBehaviour
{
    [Header("Prerequisites")]
    public DoorScript level2Door;
    public DoorScript level3Door;
    public GameObject interactPromptPrefab;

    private bool inTrigger;
    private bool hasBeenTriggered;
    private Animator animator;
    /*private DialogueStarter dialogueStarter;*/

    // Start is called before the first frame update
    void Start()
    {
        /*dialogueStarter = GetComponent<DialogueStarter>();*/
    }

    private void Update()
    {
        if (inTrigger && !hasBeenTriggered && Input.GetKeyDown(KeyCode.E))
        {
            hasBeenTriggered = true;
            interactPromptPrefab.SetActive(false);
            AltarInteract();
        }
    }

    private void AltarInteract()
    {
        switch (ItemDisplay.itemsToShow)
        {
            case 0:
                level2Door.UnlockDoor();
                break;
            case 1:
                level3Door.UnlockDoor();
                break;
            case 2:
                Debug.Log("Window Opens");
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
            interactPromptPrefab.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            interactPromptPrefab.SetActive(false);
            hasBeenTriggered = false;
            /*dialogueStarter.hasBeenTriggered = false;*/
        }
    }

}
