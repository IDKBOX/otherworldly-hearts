using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject player;
    private GameObject ghost;
    private CheckpointManager gm;

    private void Awake()
    {
        player = GameObject.Find("Character");
        ghost = GameObject.Find("FloatingGhost");
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointManager>();
    }

    private void Start()
    {
        spawnPlayer();
    }

    public void spawnPlayer()
    {
        player.transform.position = transform.position;

        if (ghost != null)
        {
            ghost.transform.position = transform.position;
        }
    }
}
