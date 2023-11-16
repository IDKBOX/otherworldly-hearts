using UnityEngine;

public class Hazard : MonoBehaviour
{
    private CheckpointManager gm;
    bool hasBeenTriggered;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointManager>();
        gm.sceneActive = gameObject.scene.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HazardCollider") && !hasBeenTriggered && !collision.gameObject.GetComponentInParent<CharacterMovement>().isDashing)
        {
            hasBeenTriggered = true;
            RespawnPlayer respawnPlayerScript = FindObjectOfType<RespawnPlayer>();
            respawnPlayerScript.respawnPlayer();
        }
    }
}
