using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PortraitDialogueScript : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public Image characterImage;

    public ScriptableCharacter characterData;
    public string[] lines, emotion;
    public float textSpeed = 0.05f;
    [HideInInspector] public bool isDialogueRunning = false;

    private int index;
    public GameObject choiceOption;

    private void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
    }

/*    void Start()
    {
        StartDialogue();
    }*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        isDialogueRunning = true;
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        EmotionChecker(emotion[index]);

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;

            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
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
            characterImage.sprite = characterData.smileSprite;
        }
        else if (emotionIndex == "Sad")
        {
            characterImage.sprite = characterData.sadSprite;
        }
        else if (emotionIndex == "Choice")
        {
            choiceOption.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            characterImage.sprite = characterData.defaultSprite;
        }
    }
}
