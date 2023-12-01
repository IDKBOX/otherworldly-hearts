using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public float loadSceneDelay;

    public void LoadScene()
    {
        StartCoroutine(StartTransition());
        SoundManager.Instance.FadeOut();
    }

    IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(loadSceneDelay);
        TransitionManager.Instance.StartTransition();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        TransitionManager.Instance.EndTransition();
    }
}
