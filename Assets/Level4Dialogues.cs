using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Dialogues : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Level4Manager.Instance.isDialogStarted)
        {
            gameObject.SetActive(false);
            RisingCorruption.FindObjectOfType<RisingCorruption>().startCorruption();
        }
    }

}
