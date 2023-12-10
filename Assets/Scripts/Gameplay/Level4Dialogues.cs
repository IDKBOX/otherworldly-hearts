using UnityEngine;

public class Level4Dialogues : MonoBehaviour
{
    public GameObject musicPlayer;

    void Start()
    {
        if (Level4Manager.Instance.isDialogStarted || CheckpointManager.Instance.checkpointActive)
        {
            musicPlayer.SetActive(true);
            RisingCorruption.FindObjectOfType<RisingCorruption>().startCorruption();
            gameObject.SetActive(false);
        }
    }

}
