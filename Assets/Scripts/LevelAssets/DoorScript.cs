using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    public string currentSceneName;
    public string nextSceneName;
    public bool isLocked;
    public bool disableFadeOutMusic;

    private bool inTrigger;
    private bool hasBeenTriggered;
    private Animator animator;

    [Header("Prerequisites")]
    public GameObject interactPromptPrefab;
    private DialogueStarter dialogueStarter;
    public AudioClip SFXOpenDoor;

    //new input system
    private PlayerControls playerControls;
    private InputAction interact;

    private void Awake()
    {
        dialogueStarter = GetComponent<DialogueStarter>();
        animator = GetComponent<Animator>();
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
        if (!isLocked && animator != null)
        {
            animator.SetTrigger("Unlock");
        }
    }

    public void LoadScene()
    {
        StartCoroutine(StartTransition());

        if (!disableFadeOutMusic)
        {
            SoundManager.Instance.FadeOut();
        }
    }

    IEnumerator StartTransition()
    {
        TransitionManager.Instance.StartTransition();
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
        CheckpointManager.Instance.resetCheckpoint();
        TransitionManager.Instance.EndTransition();
    }

    private void Update()
    {
        if (inTrigger && !hasBeenTriggered && interact.triggered)
        {
            hasBeenTriggered = true;
            interactPromptPrefab.SetActive(false);
            ShowInteractButton.Instance.DisableInteractButton();

            if (isLocked)
            {
                dialogueStarter.StartDialogue();
            }
            else
            {
                SoundManager.Instance.PlaySound(SFXOpenDoor);
                LoadScene();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = true;
            interactPromptPrefab.SetActive(true);
            ShowInteractButton.Instance.EnableInteractButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            interactPromptPrefab.SetActive(false);
            hasBeenTriggered = false;
            dialogueStarter.hasBeenTriggered = false;
            ShowInteractButton.Instance.DisableInteractButton();
        }
    }

    public void UnlockDoor()
    {
        animator.SetTrigger("Unlock");
        isLocked = false;
    }
}
