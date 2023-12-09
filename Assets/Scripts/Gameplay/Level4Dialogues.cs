using UnityEngine;

public class Level4Dialogues : MonoBehaviour
{
    void Start()
    {
        if (Level4Manager.Instance.isDialogStarted)
        {
            gameObject.SetActive(false);
            RisingCorruption.FindObjectOfType<RisingCorruption>().startCorruption();
        }
    }

}
