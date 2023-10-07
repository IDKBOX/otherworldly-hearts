using System.Collections;
using UnityEngine;
using TMPro;

public class DescriptionDialogueScript : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    [HideInInspector] public ScriptableDialogue dialogueData;

    public float textSpeed = 0.05f;
    public AudioSource dialogueSound;
    [HideInInspector] public bool isDialogueRunning = false;

    private int index;

    private void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogueData.isDescriptionDialogue)
        {
            if (textComponent.text == dialogueData.lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialogueData.lines[index];
            }
        }
    }

    public void StartDialogue(ScriptableDialogue _dialogueData)
    {
        dialogueData = _dialogueData;
        isDialogueRunning = true;
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in dialogueData.lines[index].ToCharArray())
        {
            textComponent.text += c;
            dialogueSound.Play();

            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogueData.lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            isDialogueRunning = false;
            gameObject.SetActive(false);
        }
    }
}
