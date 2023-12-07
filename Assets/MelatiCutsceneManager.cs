using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MelatiCutsceneManager : MonoBehaviour
{
    public float animationTime;
    public string currentSceneName;
    public string nextSceneName;

    IEnumerator Start()
    {
        deleteData();
        yield return new WaitForSeconds(animationTime);
        SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
        CheckpointManager.Instance.resetCheckpoint();
    }

    public void deleteData()
    {
        CheckpointManager.Instance.deleteCheckpointData();
        PlayerPrefs.DeleteKey("StoryItemData");
    }
}
