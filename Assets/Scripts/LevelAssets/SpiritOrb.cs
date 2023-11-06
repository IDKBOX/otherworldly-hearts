using DG.Tweening;
using UnityEngine;

public class SpiritOrb : MonoBehaviour
{
    public SpiritGate linkedSpiritGate;
    private ParticleSystem collectEffect;

    private void Awake()
    {
        collectEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            linkedSpiritGate.spiritOrbsRequired--;
            collectEffect.Play();

            //destroy when touched
            transform.DOScale(Vector3.zero, 0.5f);
            Destroy(GetComponentInChildren<SpriteRenderer>().gameObject, 0.5f);
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(gameObject, 3f);
        }
    }
}
