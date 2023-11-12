using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private GameMaster gm;
    //public GameObject player;
    //CharacterMovement playerScript;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //player = GameObject.Find("Character");
        //playerScript = FindAnyObjectByType<CharacterMovement>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //playerScript.SpawnPoint = gameObject.transform;
            gm.lastCheckPointPos = transform.position;
        }
    }

    //public void spawnPlayer()
    //{
    //    //player.transform.position = playerScript.SpawnPoint.position;
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}

