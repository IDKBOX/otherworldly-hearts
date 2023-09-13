using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PortraitDialogueScript : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public Image characterImage;
    [HideInInspector] public ScriptableDialogue dialogueData;

    public float textSpeed = 0.05f;
    [HideInInspector] public bool isDialogueRunning = false;

    private int index;

    private void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        EmotionChecker(dialogueData.emotion[index]);

        foreach (char c in dialogueData.lines[index].ToCharArray())
        {
            textComponent.text += c;

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

    void EmotionChecker(string emotionIndex)
    {
        if (emotionIndex == "Smile")
        {
            characterImage.sprite = dialogueData.characterData.smileSprite;
        }
        else if (emotionIndex == "Sad")
        {
            characterImage.sprite = dialogueData.characterData.sadSprite;
        }
        else if (emotionIndex == "Choice")
        {
            dialogueData.choiceOption.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            characterImage.sprite = dialogueData.characterData.defaultSprite;
        }
    }
}
