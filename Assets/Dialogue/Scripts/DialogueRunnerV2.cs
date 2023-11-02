using UnityEngine;

public class DialogueRunnerV2 : MonoBehaviour
{
    public ScriptableDialogue[] dialogueData;
    private bool isDialogueRunnerRunning;
    private int index = 0;

    [Header("Prerequisites")]
    public GameObject DialogueUIPrefab;
    public DescriptionDialogueScript descriptionDialoguePrefab;
    public PortraitDialogueScript portraitDialoguePrefab;

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && isDialogueRunnerRunning == false)
        {
            index = 0;
            isDialogueRunnerRunning = true;

            StartDialogueRunner();
        }

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
                    isDialogueRunnerRunning = false;
                    DialogueUIPrefab.SetActive(false);
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
                    isDialogueRunnerRunning = false;
                    DialogueUIPrefab.SetActive(false);
                }
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
        else if (dialogueData[index].isPortraitDialogue)
        {
            portraitDialoguePrefab.gameObject.SetActive(true);
            portraitDialoguePrefab.StartDialogue(dialogueData[index]);
        }
    }
}
