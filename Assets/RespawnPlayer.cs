using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    private CheckpointManager gm;
    
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointManager>();
        transform.position = gm.lastCheckPointPos;
    }

    public void respawnPlayer()
    {
        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    {
        TransitionManager.Instance.StartTransition();
        yield return new WaitForSeconds(1f);

        SceneManager.UnloadSceneAsync(gm.sceneActive);
        SceneManager.LoadScene(gm.sceneActive, LoadSceneMode.Additive);
        transform.position = gm.lastCheckPointPos;

        TransitionManager.Instance.EndTransition();
    }
}
