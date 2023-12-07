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
