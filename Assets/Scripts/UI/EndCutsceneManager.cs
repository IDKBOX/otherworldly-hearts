using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutsceneManager : MonoBehaviour
{
    private GameObject dialogueCanvas;
    private GameObject level4Manager;

    public void ReturnToMainMenu()
    {
        StartCoroutine(StartTransition());
        SoundManager.Instance.FadeOut();
        
        if (dialogueCanvas != null)
        {
            Destroy(dialogueCanvas);
            Destroy(level4Manager);
        }
    }

    IEnumerator StartTransition()
    {
        TransitionManager.Instance.StartTransition();
        yield return new WaitForSeconds(1f);
        TransitionManager.Instance.EndTransition();

        {
            SceneManager.LoadScene(0);
        }
    }

    public void Start()
    {
        CheckpointManager.Instance.deleteCheckpointData();
        CheckpointManager.Instance.loadCheckpointData();
        PlayerPrefs.DeleteKey("StoryItemData");
        dialogueCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        level4Manager = GameObject.FindGameObjectWithTag("Level4Manager");
    }
}
