using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    private CheckpointManager gm;
    private CharacterMovement characterMovement;
    private Animator animator;

    public GameObject floatingGhost;
    public GameObject vortexEffect;
    public AudioClip SFXDeath;

    private bool sceneReloaded;
    private bool isDead;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointManager>();
        transform.position = gm.lastCheckPointPos;
    }

    public void respawnPlayer()
    {
        CinemachineShake.Instance.ShakeCamera(15, 0.1f);
        if (!isDead)
        {
            isDead = true;
            StartCoroutine(StartTransition());
        }
    }

    IEnumerator StartTransition()
    {
        characterMovement.isDisabled = true;
        animator.SetTrigger("Death");
        Instantiate(vortexEffect, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SFXDeath);
        characterMovement.rb.velocity = Vector2.zero;
        characterMovement.rb.simulated = false;
        TransitionManager.Instance.StartTransition();
        PauseManager.canPause = false;
        yield return new WaitForSeconds(1f);

        if (!sceneReloaded)
        {
            sceneReloaded = true;
            SceneManager.UnloadSceneAsync(gm.sceneActive);
            SceneManager.LoadScene(gm.sceneActive, LoadSceneMode.Additive);
        }
        
        transform.position = gm.lastCheckPointPos;
        floatingGhost.transform.position = transform.position;

        TransitionManager.Instance.EndTransition();
        PauseManager.canPause = true;

        yield return new WaitForSeconds(0.5f);
        characterMovement.rb.simulated = true;
        characterMovement.isDisabled = false;
        sceneReloaded = false;
        isDead = false;
    }
}