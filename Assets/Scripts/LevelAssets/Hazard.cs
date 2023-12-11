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
        if (collision.gameObject.CompareTag("HazardCollider") && !hasBeenTriggered && !collision.gameObject.GetComponentInParent<CharacterMovement>().isInvincible)
        {
            hasBeenTriggered = true;
            RespawnPlayer respawnPlayerScript = FindObjectOfType<RespawnPlayer>();
            respawnPlayerScript.respawnPlayer();

            gm.saveDeathCount((PlayerPrefs.GetInt("DeathCount") + 1 ));
            gm.textDeath.text = ""+PlayerPrefs.GetInt("DeathCount");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HazardCollider") && !hasBeenTriggered && !collision.gameObject.GetComponentInParent<CharacterMovement>().isInvincible)
        {
            hasBeenTriggered = true;
            RespawnPlayer respawnPlayerScript = FindObjectOfType<RespawnPlayer>();
            respawnPlayerScript.respawnPlayer();
        }
    }
}
