using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    private CheckpointManager gm;
    private CharacterMovement characterMovement;
    public GameObject ghost;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointManager>();
        transform.position = gm.lastCheckPointPos;
        characterMovement = GetComponent<CharacterMovement>();
    }

    public void respawnPlayer()
    {
        CinemachineShake.Instance.ShakeCamera(15, 0.1f);
        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    {
        characterMovement.isDisabled = true;
        characterMovement.rb.velocity = Vector2.zero;
        TransitionManager.Instance.StartTransition();
        yield return new WaitForSeconds(1f);

        SceneManager.UnloadSceneAsync(gm.sceneActive);
        SceneManager.LoadScene(gm.sceneActive, LoadSceneMode.Additive);
        transform.position = gm.lastCheckPointPos;
        ghost.transform.position = transform.position;

        characterMovement.isDisabled = false;
        TransitionManager.Instance.EndTransition();
    }
}
