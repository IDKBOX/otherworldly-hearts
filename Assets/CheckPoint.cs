using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("On");
            CheckpointManager.Instance.lastCheckPointPos = transform.position;
            CheckpointManager.Instance.checkpointActive = true;
            CheckpointManager.Instance.saveCheckpoint();
        }
    }
}

