using UnityEngine;

public class DialogueRunnerV2 : MonoBehaviour
{
    ScriptableDialogue[] dialogueData;
    private bool isDialogueRunnerRunning;
    private int index = 0;
    public static DialogueRunnerV2 Instance;

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
            else if (dialogueData[index].isPortraitDialogue)
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
            }
            
            Destroy(currentDialogueTrigger, 1f);
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
        else if (dialogueData[index].isPortraitDialogue)
        {
            portraitDialoguePrefab.gameObject.SetActive(true);
            portraitDialoguePrefab.StartDialogue(dialogueData[index]);
        }
    }
}
