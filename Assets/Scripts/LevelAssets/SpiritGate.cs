using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiritGate : MonoBehaviour
{
    public int spiritOrbsRequired;
    public float gateMoveRange = 4f;
    bool tweenPlayed = false;

    void Update()
    {
        if (spiritOrbsRequired == 0)
        {
            if (!tweenPlayed)
            {
                tweenPlayed = true;
                transform.DOMoveY(transform.position.y + gateMoveRange, 0.5f);
            }
        }
    }
}
