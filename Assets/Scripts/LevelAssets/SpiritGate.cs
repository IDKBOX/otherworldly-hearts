using DG.Tweening;
using UnityEngine;

public class SpiritGate : MonoBehaviour
{
    public int spiritOrbsRequired;
    bool tweenPlayed = false;

    void Update()
    {
        if (spiritOrbsRequired == 0)
        {
            if (!tweenPlayed)
            {
                tweenPlayed = true;
                transform.DOLocalMoveY(4, 0.5f);
            }
        }
    }
}
