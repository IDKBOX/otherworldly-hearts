using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
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
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Additive);
        TransitionManager.Instance.EndTransition();
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Z))
    //    {
    //        SceneManager.UnloadSceneAsync("Base");
    //        SceneManager.LoadScene("Base", LoadSceneMode.Additive);
    //    }
    //}
}
