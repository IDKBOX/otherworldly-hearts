using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject player;
    private GameObject ghost;

    private void Awake()
    {
        player = GameObject.Find("Character");
        ghost = GameObject.Find("FloatingGhost");
    }

    private void Start()
    {
        if (!CheckpointManager.Instance.checkpointActive)
        {
            spawnPlayer();
        }
    }

    public void spawnPlayer()
    {
        CheckpointManager.Instance.lastCheckPointPos = transform.position;
        player.transform.position = transform.position;

        if (ghost != null)
        {
            ghost.transform.position = transform.position;
        }
    }
}
