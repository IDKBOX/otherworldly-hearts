using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    public string[] dialogueArray;

    public GameObject descriptionDialogue;
    public PortraitDialogueScript portraitDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && portraitDialogue.isDialogueRunning == false)
        {
            portraitDialogue.gameObject.SetActive(true);
            portraitDialogue.GetComponent<PortraitDialogueScript>().StartDialogue();
        }
    }
}
