using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.5f;

    private int index;
    [HideInInspector] public bool isDialogueRunning = false;

    private void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
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

    void StartDialogue()
    {
        isDialogueRunning = true;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
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
}
