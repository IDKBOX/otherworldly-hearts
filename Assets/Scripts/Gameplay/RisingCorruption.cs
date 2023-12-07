using DG.Tweening;
using UnityEngine;

public class RisingCorruption : MonoBehaviour
{
    public float timeLimit;
    //[Header("Dialog Trigger")]
    //public DialogueStarter Level4firstDialog;

    void Start()
    {

        if (CheckpointManager.Instance.checkpointActive)
        {
            transform.position = new Vector3(0, CheckpointManager.Instance.lastCheckPointPos.y, 0);
        }
        else
        {
            transform.position = Vector3.zero;
        }

        //Level4firstDialog.StartDialogue();
       
        ////GameObject gm = GameObject.FindGameObjectWithTag("GM");
        ////Debug.Log(gm);
        //startCorruption();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.L))
        {
            stopCorruption();
        }
    }
    public void stopCorruption()
    {
        transform.DOMoveY(0, timeLimit);
    }

    public void startCorruption()
    {
        transform.DOMoveY(159, timeLimit);
    }

    public void startCorruptionSlowly()
    {
        transform.DOMoveY(5, timeLimit);
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
