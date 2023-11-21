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
    public AudioClip dialogueSound;
    [HideInInspector] public bool isDialogueRunning = false;

    private int index;

    private void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !dialogueData.isDescriptionDialogue && Time.timeScale != 0)
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
            SoundManager.Instance.PlaySound(dialogueSound);

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

    void EmotionChecker(int emotionIndex)
    {
        if (emotionIndex > 0 && emotionIndex < dialogueData.characterData.portraitImages.Length)
        {
            characterImage.sprite = dialogueData.characterData.portraitImages[emotionIndex];
        }
        else
        {
            characterImage.sprite = dialogueData.characterData.portraitImages[0];
        }
       
    }
}
