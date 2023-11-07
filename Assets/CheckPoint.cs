using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject player;
    CharacterMovement playerScript;

    private void Start()
    {
        player = GameObject.Find("Character");
        playerScript = FindAnyObjectByType<CharacterMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript.SpawnPoint = gameObject.transform;
        }
    }

    public void spawnPlayer()
    {
        player.transform.position = playerScript.SpawnPoint.position;
    }
}

