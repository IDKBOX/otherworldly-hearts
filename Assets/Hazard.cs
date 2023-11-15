using UnityEngine;

public class Hazard : MonoBehaviour
{
    private CheckpointManager gm;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointManager>();
        gm.sceneActive = gameObject.scene.name;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get PlayerPos
            RespawnPlayer respawnPlayerScript = FindObjectOfType<RespawnPlayer>();
            respawnPlayerScript.respawnPlayer();
        }
    }
}
