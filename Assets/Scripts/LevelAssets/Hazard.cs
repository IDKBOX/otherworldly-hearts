using UnityEngine;

public class Hazard : MonoBehaviour
{
    private CheckpointManager gm;
    bool hasBeenTriggered;
    private DeathCounter deathCounter;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointManager>();
        gm.sceneActive = gameObject.scene.name;
        deathCounter = GameObject.FindAnyObjectByType<DeathCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HazardCollider") && !hasBeenTriggered && !collision.gameObject.GetComponentInParent<CharacterMovement>().isInvincible)
        {
            hasBeenTriggered = true;
            RespawnPlayer respawnPlayerScript = FindObjectOfType<RespawnPlayer>();
            respawnPlayerScript.respawnPlayer();

            deathCounter.saveDeathCount((PlayerPrefs.GetInt("DeathCount") + 1 ));
            deathCounter.textDeath.text = PlayerPrefs.GetInt("DeathCount").ToString();
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
