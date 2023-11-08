using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public string currentSceneName;
    public string nextSceneName;
    public bool isLocked;

    private bool inTrigger;
    private bool hasBeenTriggered;
    private Animator animator;

    [Header("Prerequisites")]
    public GameObject interactPromptPrefab;
    private DialogueStarter dialogueStarter;

    private void Awake()
    {
        dialogueStarter = GetComponent<DialogueStarter>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!isLocked)
        {
            animator.SetTrigger("Unlock");
        }
    }

    public void LoadScene()
    {
        StartCoroutine(StartTransition());
        SoundManager.Instance.FadeOut();
    }

    IEnumerator StartTransition()
    {
        TransitionManager.Instance.StartTransition();
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
        TransitionManager.Instance.EndTransition();
    }

    private void Update()
    {
        if (inTrigger && !hasBeenTriggered && Input.GetKeyDown(KeyCode.E))
        {
            hasBeenTriggered = true;
            interactPromptPrefab.SetActive(false);

            if (isLocked)
            {
                dialogueStarter.StartDialogue();
            }
            else
            {
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
        }
    }

    public void UnlockDoor()
    {
        animator.SetTrigger("Unlock");
        isLocked = false;
    }
}
