using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    public AudioClip SFXCheckpointActive;
    private bool activated;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            activated = true;
            animator.SetTrigger("On");
            SoundManager.Instance.PlaySound(SFXCheckpointActive);
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            CheckpointManager.Instance.lastCheckPointPos = transform.position;
            CheckpointManager.Instance.checkpointActive = true;
            CheckpointManager.Instance.saveCheckpoint();
        }
    }
}

