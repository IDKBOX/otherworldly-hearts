using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroRunner : MonoBehaviour
{
    public string[] dialogueText;
    public Sprite[] dialogueImages;

    [Header("Prerequisites")]
    public GameObject introCanvas;
    public TextMeshProUGUI textComponent;
    public Image dialogueImageObject;
    private float textSpeed = 0.05f;
    public AudioClip dialogueSound;
    [HideInInspector] public bool isDialogueRunning = false;
    public Animator transition;

    private int index;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == dialogueText[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialogueText[index];
            }
        }
    }

    public void StartDialogue()
    {
        introCanvas.SetActive(true);
        isDialogueRunning = true;
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        ImageChanger();

        foreach (char c in dialogueText[index].ToCharArray())
        {
            textComponent.text += c;
            SoundManager.Instance.PlaySound(dialogueSound);

            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogueText.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            LoadScene();
        }
    }

    void ImageChanger()
    {
        dialogueImageObject.sprite = dialogueImages[index];
    }


    //start script
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        StartDialogue();
    }

    //load main menu
    public void LoadScene()
    {
        StartCoroutine(StartTransition());
        SoundManager.Instance.FadeOut();
    }

    IEnumerator StartTransition()
    {
        transition.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
