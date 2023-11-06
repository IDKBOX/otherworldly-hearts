using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    ScriptableDialogue[] dialogueData;
    private bool isDialogueRunnerRunning;
    private int index = 0;
    public static DialogueRunner Instance;

    [Header("Prerequisites")]
    public GameObject DialogueUIPrefab;
    public DescriptionDialogueScript descriptionDialoguePrefab;
    public PortraitDialogueScript portraitDialoguePrefab;

    [HideInInspector] public GameObject currentDialogueTrigger;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerDialogue(ScriptableDialogue[] _dialogueData)
    {
        if (isDialogueRunnerRunning == false)
        {
            dialogueData = _dialogueData;

            index = 0;
            isDialogueRunnerRunning = true;

            StartDialogueRunner();
        }
    }

    private void Update()
    {
        if (isDialogueRunnerRunning)
        {
            if (dialogueData[index].isDescriptionDialogue)
            {
                if (!descriptionDialoguePrefab.isActiveAndEnabled && isDialogueRunnerRunning)
                {
                    if (index < dialogueData.Length - 1)
                    {
                        index++;
                        StartDialogueRunner();
                    }
                    else
                    {
                        OnDialogueFinished();
                    }
                }
            }
            else if (!dialogueData[index].isDescriptionDialogue)
            {
                if (!portraitDialoguePrefab.isActiveAndEnabled && isDialogueRunnerRunning)
                {
                    if (index < dialogueData.Length - 1)
                    {
                        index++;
                        StartDialogueRunner();
                    }
                    else
                    {
                        OnDialogueFinished();
                    }
                }
            }
        }
    }

    private void OnDialogueFinished()
    {
        isDialogueRunnerRunning = false;
        DialogueUIPrefab.SetActive(false);
        FindObjectOfType<CharacterMovement>().isDisabled = false;

        if (currentDialogueTrigger != null)
        {
            if (currentDialogueTrigger.GetComponent<CinemachineFocus>() != null)
            {
                currentDialogueTrigger.GetComponent<CinemachineFocus>().EndCinemachineFocus();
                Destroy(currentDialogueTrigger, 1f);
            }
            else
            {
                Destroy(currentDialogueTrigger);
            }
        }
    }

    private void StartDialogueRunner()
    {
        DialogueUIPrefab.SetActive(true);

        if (dialogueData[index].isDescriptionDialogue)
        {
            descriptionDialoguePrefab.gameObject.SetActive(true);
            descriptionDialoguePrefab.StartDialogue(dialogueData[index]);
        }
        else if (!dialogueData[index].isDescriptionDialogue)
        {
            portraitDialoguePrefab.gameObject.SetActive(true);
            portraitDialoguePrefab.StartDialogue(dialogueData[index]);
        }
    }
}
