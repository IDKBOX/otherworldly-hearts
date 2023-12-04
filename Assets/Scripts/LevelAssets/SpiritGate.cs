using DG.Tweening;
using UnityEngine;

public class SpiritGate : MonoBehaviour
{
    public int spiritOrbsRequired;
    public float gateMoveRange = 4f;
    public bool horizontalGate;
    public AudioClip SFXSpiritGateOpen;
    bool tweenPlayed = false;

    void Update()
    {
        if (spiritOrbsRequired == 0 && !tweenPlayed)
        {
            tweenPlayed = true;
            SoundManager.Instance.PlaySound(SFXSpiritGateOpen);

            if (!horizontalGate)
            {
                transform.DOMoveY(transform.position.y + gateMoveRange, 0.5f);
            }
            else
            {
                transform.DOMoveX(transform.position.x + gateMoveRange, 0.5f);
            }
        }
    }
}
