using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string currentSceneName;
    public string nextSceneName;
    public float loadSceneDelay;
    public bool useTransitionManager;
    public bool loadAdditive;

    public void LoadScene()
    {

        StartCoroutine(StartTransition());
        SoundManager.Instance.FadeOut();
    }

    IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(loadSceneDelay);
        if (useTransitionManager)
        {
            TransitionManager.Instance.StartTransition();
            yield return new WaitForSeconds(1f);
        }

        if (!loadAdditive)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            SceneManager.UnloadSceneAsync(currentSceneName);
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
            CheckpointManager.Instance.resetCheckpoint();
        }

        if (useTransitionManager)
        {
            TransitionManager.Instance.EndTransition();
        }
    }
}
