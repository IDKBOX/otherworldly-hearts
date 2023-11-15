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
        Debug.Log(gm.sceneActive);
        StartCoroutine(StartTransition());

    }

    IEnumerator StartTransition()
    {
        TransitionManager.Instance.StartTransition();
        yield return new WaitForSeconds(1f);

        string curentSceneUnload = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(gm.sceneActive);
        SceneManager.LoadScene(gm.sceneActive, LoadSceneMode.Additive);
        transform.position = gm.lastCheckPointPos;

        TransitionManager.Instance.EndTransition();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        //SceneManager.UnloadSceneAsync("Base");
    //        //SceneManager.LoadScene("Base", LoadSceneMode.Additive);
    //        respawnPlayer();
    //    }
    //}
}
