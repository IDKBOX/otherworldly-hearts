using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutsceneManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        StartCoroutine(StartTransition());
        SoundManager.Instance.FadeOut();
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
    }
}
