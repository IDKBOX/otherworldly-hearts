using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
        }
    }
}

