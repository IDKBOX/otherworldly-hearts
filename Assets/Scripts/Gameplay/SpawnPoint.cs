using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Character");
    }

    private void Start()
    {
        spawnPlayer();
    }

    public void spawnPlayer()
    {
        player.transform.position = transform.position;
    }
}
