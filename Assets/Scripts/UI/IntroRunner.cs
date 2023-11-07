using DG.Tweening;
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
    public Image dialogueImageObjectBG;
    public float textSpeed = 0.05f;
    public AudioClip dialogueSound;
    [HideInInspector] public bool isDialogueRunning = false;
    public Animator transition;

    private int index;

    //dialogue code
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
            ImageChanger();
            ChangeDialogueTween();
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            LoadScene();
        }
    }

    //image changing script
    void ImageChanger()
    {
        dialogueImageObject.sprite = dialogueImages[index];

        if (index != 0)
        {
            dialogueImageObjectBG.sprite = dialogueImages[index - 1];
        }
    }


    //start script
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        ChangeDialogueTween();
        StartDialogue();
    }

    //load next scene
    private void LoadScene()
    {
        DOTween.KillAll();
        StartCoroutine(StartTransition());
        SoundManager.Instance.FadeOut();
    }

    IEnumerator StartTransition()
    {
        transition.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Base");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    }

    private void ChangeDialogueTween()
    {
        //0 opacity instantly then slowly fades back up
        if (index != 1 && index != 3 && index != 5 && index != 7)
        {
            Sequence imageFade = DOTween.Sequence();

            imageFade.Append(dialogueImageObject.DOColor(new Color32(255, 255, 255, 0), 0f).SetEase(Ease.Linear))
            .Append(dialogueImageObject.DOColor(new Color32(255, 255, 255, 255), 2.5f));
        }

        //enable image BG for seamless crossfade
        if (index == 2 || index == 3)
        {
            dialogueImageObjectBG.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            dialogueImageObjectBG.color = new Color32(255, 255, 255, 0);
        }

        //index 3 fade out
        if (index == 3)
        {
            Sequence imageFade = DOTween.Sequence();

            imageFade.Append(dialogueImageObject.DOColor(new Color32(255, 255, 255, 0), 0f).SetEase(Ease.Linear))
            .Append(dialogueImageObject.DOColor(new Color32(255, 255, 255, 255), 1.5f))
            .Join(dialogueImageObjectBG.DOColor(new Color32(255, 255, 255, 0), 1.5f));
        }

        if (index == 7)
        {
            dialogueImageObject.DOColor(new Color32(255, 255, 255, 125), 1.5f);
        }
    }
}
