using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject player;
    private GameObject ghost;
    public AudioClip SFXSpawn;
    public bool playAudio;

    private void Awake()
    {
        player = GameObject.Find("Character");
        ghost = GameObject.Find("FloatingGhost");
    }

    private void Start()
    {
        if (!CheckpointManager.Instance.checkpointActive)
        {
            SpawnPlayer();
            CheckpointManager.Instance.saveLevel();
        }
    }

    public void SpawnPlayer()
    {
        CheckpointManager.Instance.lastCheckPointPos = transform.position;
        player.transform.position = transform.position;
        CheckpointManager.Instance.checkpointActive = true;

        if (playAudio && SFXSpawn != null)
        {
            SoundManager.Instance.PlaySound(SFXSpawn);
        }

        if (ghost != null)
        {
            ghost.transform.position = transform.position;
        }
    }
}
