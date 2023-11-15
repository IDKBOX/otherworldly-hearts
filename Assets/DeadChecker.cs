using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadChecker : MonoBehaviour
{
    private GameMaster gm;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        gm.sceneActive = gameObject.scene.name;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check dead
        if (collision.gameObject.CompareTag("Player"));
        {
            // Get PlayerPos
            RespawnPlayer playerPosScript = FindObjectOfType<RespawnPlayer>();
            playerPosScript.respawnPlayer();
        }
    }
}
