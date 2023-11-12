using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }

    public void respawnPlayer()
    {
        //scenemanager.loadscene(scenemanager.getactivescene().buildindex);
        SceneManager.UnloadSceneAsync("Base");
        SceneManager.LoadScene("Base", LoadSceneMode.Additive);

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
