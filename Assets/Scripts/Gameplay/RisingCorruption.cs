using DG.Tweening;
using UnityEngine;

public class RisingCorruption : MonoBehaviour
{
    public float timeLimit;

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

        transform.DOMoveY(159, timeLimit);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
